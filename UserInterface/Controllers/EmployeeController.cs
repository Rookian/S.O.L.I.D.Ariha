using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Core.Domain.Bases.Repositories;
using Core.Domain.Model;
using Core.Interfaces;
using UserInterface.ActionResults;
using UserInterface.Controllers.Base;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class EmployeeController : ConventionController
    {
        private readonly ITeamEmployeeRepository _teamEmployeeRepository;
        private readonly IMappingService _mappingService;

        public EmployeeController(ITeamEmployeeRepository teamEmployeeRepository, IMappingService mappingService)
        {
            _teamEmployeeRepository = teamEmployeeRepository;
            _mappingService = mappingService;
        }

        public ActionResult Index([DefaultValue(1)] int page)
        {
            var list = _teamEmployeeRepository.GetPagedTeamEmployees(page, PageSize);
            return new AutoMappedHybridViewResult<TeamEmployee, TeamEmployeeForm>(list);
        }

        public ActionResult Create()
        {
            return AutoMappedView<TeamEmployeeInput>(new TeamEmployee());
        }

        [HttpPost]
        public ActionResult Create(TeamEmployeeInput teamEmployeeInput)
        {
            return Command<TeamEmployeeInput, TeamEmployee>(teamEmployeeInput,
                s => RedirectToAction<EmployeeController>(x => x.Index(1)),
                f => RedirectToAction<EmployeeController>(x => x.Index(1)));
        }

        public ActionResult Delete(DeleteTeamEmployeeInput deleteTeamEmployeeInput)
        {
            return Command<DeleteTeamEmployeeInput, TeamEmployee>(deleteTeamEmployeeInput,
                s => RedirectToAction<EmployeeController>(x => x.Index(1)),
                f => RedirectToAction<EmployeeController>(x => x.Index(1)));
        }

        public ActionResult Edit(int id)
        {
            return AutoMappedView<TeamEmployeeInput>(_teamEmployeeRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(TeamEmployeeInput teamEmployeeInput)
        {
            return Command<TeamEmployeeInput, TeamEmployee>(teamEmployeeInput,
                s => RedirectToAction<EmployeeController>(x => x.Index(1)),
                f => View(teamEmployeeInput));
        }

        public ActionResult Reminders()
        {
            ReminderService reminderService = new ReminderService();
            ReminderType[] reminderTypes = reminderService.GetReminderTypes();

            SelectListItem[] selectListItems = _mappingService.Map(reminderTypes, reminderTypes.GetType(), typeof(SelectListItem[])) as SelectListItem[];

            string time = DateTime.Now.ToString();

            DateTime dateTime = (DateTime) _mappingService.Map(time, typeof (string), typeof (DateTime));

            RemindersViewModel remindersViewModel = new RemindersViewModel
                                                    {
                                                        Reminder = selectListItems,
                                                        SelectedReminder = ReminderType.CReminder
                                                    };

            return View(remindersViewModel);

        }
    }

    public class RemindersViewModel
    {
        public SelectListItem[] Reminder { get; set; }
        public ReminderType SelectedReminder { get; set; }
    }

    public enum ReminderType
    {
        AReminder = 1,
        BReminder = 2,
        CReminder = 3
    }

    public class ReminderService
    {
        public ReminderType[] GetReminderTypes()
        {
            return Enum.GetValues(typeof(ReminderType)).Cast<object>().Cast<ReminderType>().ToArray();
        }
    }
}