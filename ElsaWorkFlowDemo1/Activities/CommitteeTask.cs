//using Elsa.ActivityResults;
//using Elsa.Attributes;
//using Elsa.Design;
//using Elsa.Services;
//using Elsa.Services.Models;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using MApp.Framework.Application.UserManagement;
//using MApp.Framework.Application.Workflow.Bookmarks;
//using MApp.Framework.Application.Workflow.Commands;
//using MApp.Framework.Application.Workflow.DataProviders;
//using MApp.Framework.Application.Workflow.Queries;
//using MApp.Framework.Domain.UserManagement;
//namespace using Elsa.ActivityResults;
//using Elsa.Attributes;
//using Elsa.Design;
//using Elsa.Services;
//using Elsa.Services.Models;
//using Elsa.Models;
//using MediatR;

//namespace ElsaWorkFlowDemo1.Activities
//{
//    [Trigger(Category = "Tasks", Description = "Triggers when a user action is received.", Outcomes = new string[]
//   {
//        "x => x.state.actions"
//   })]
//    public class UserTask : Activity
//    {
//        private readonly ICurrentUser _currentUser;
//        private readonly IMediator _mediator;
//        private readonly IInboxQueries _inboxQueries;
//        private TaskInboxQueryResult CurrentInboxItem { get; set; }
//        public UserTask(ICurrentUser currentUser, IMediator mediator, IInboxQueries inboxQueries)
//        {
//            _currentUser = currentUser;
//            _mediator = mediator;
//            _inboxQueries = inboxQueries;
//        }
//        [ActivityInput(Name = "Permission", DefaultValueProvider
//= typeof(PermissionsProvider), UIHint = ActivityInputUIHints.Dropdown, OptionsProvider = typeof(PermissionsProvider))]
//        public string Permission { get; set; }
//        [ActivityInput(UIHint = ActivityInputUIHints.MultiText, Hint = "Provide a list of available actions", DefaultSyntax = "Json", SupportedSyntaxes = new string[]
//        {
//            "Json",
//            "JavaScript",
//            "Liquid"
//        }, OptionsProvider = typeof(ActionsProvider))]
//        public ICollection<string> Actions
//        {
//            get;
//            set;
//        }

//        [ActivityInput(Name = "ViewName", UIHint = ActivityInputUIHints.SingleLine)]
//        public string ViewName { get; set; }

//        [ActivityInput(UIHint = ActivityInputUIHints.Checkbox, Hint = "Check this option if there is some extra data to be saved")]
//        public bool SaveExtraData
//        {
//            get;
//            set;
//        }
//        [ActivityInput(UIHint = ActivityInputUIHints.Checkbox, Hint = "Check this option if there is a committee to be assigned at to a certain activity")]
//        public bool IsCommitteForming
//        {
//            get;
//            set;
//        }
//        [ActivityOutput]
//        public string Output
//        {
//            get;
//            set;
//        }
//        [ActivityInput(UIHint = ActivityInputUIHints.SingleLine, Hint = "The Activity Id in which the committe head must take action at", SupportedSyntaxes = new string[]
//        {
//            "JavaScript",
//        })]
//        public string AssignedToActivity { get; set; }

//        protected async override ValueTask<bool> OnCanExecuteAsync(ActivityExecutionContext context)
//        {
//            var bookmarkData = context.GetInput<TaskBookmark>();
//            if (context.WorkflowExecutionContext.IsFirstPass && bookmarkData == null)
//                return true;
//            var workflowInstance = context.WorkflowInstance;
//            var currentBlockingAction = workflowInstance.BlockingActivities.FirstOrDefault()?.ActivityId ?? null;
//            CurrentInboxItem = (await _inboxQueries.GetCurrentUserInbox(null, null, null, context.WorkflowInstance.Id, currentBlockingAction)).Data.FirstOrDefault();
//            if (currentBlockingAction == null)
//            {
//                return false;
//            }
//            var activityData = workflowInstance.ActivityData[currentBlockingAction.ToString()];
//            if (!(await _currentUser.HasPermission(((Permission)Enum.Parse(typeof(Permission), activityData[WorkflowKeys.Permission]?.ToString() ?? MApp.Framework.Domain.UserManagement.Permission.Unspecified.ToString())), true)) && (CurrentInboxItem == null || CurrentInboxItem.Status != ActivityStatus.Active))
//                return false;
//            return true;
//        }
//        protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
//        {
//            return Suspend();
//        }
//        async protected override ValueTask<IActivityExecutionResult> OnResumeAsync(ActivityExecutionContext context)
//        {
//            var workflowInstance = context.WorkflowInstance;
//            var activityData = workflowInstance.ActivityData[context.ActivityId];
//            var actions = (List<string>)activityData[WorkflowKeys.Actions];

//            if (SaveExtraData)
//            { context.SetVariable(context.ActivityId, context.GetInput<TaskBookmark>().ActivityPayload); }

//            if (IsCommitteForming)
//            {
//                if (!context.GetInput<TaskBookmark>().CommitteeId.HasValue)
//                    return Suspend();

//                await _mediator.Publish(new WorkflowCommitteSelectedEvent(context.WorkflowInstance.Id, this.AssignedToActivity,
//                   context.GetInput<TaskBookmark>().CommitteeId.HasValue ? context.GetInput<TaskBookmark>().CommitteeId.Value : context.WorkflowInstance.Variables.Get<int>(WorkflowKeys.CommitteeId)));
//            }

//            if (!actions.Contains(context.GetInput<TaskBookmark>().ActionName.ToString()) && (CurrentInboxItem == null || CurrentInboxItem.Status != ActivityStatus.Active))
//            {
//                return Suspend();
//            }
//            return Outcome(context.GetInput<TaskBookmark>().ActionName.ToString());
//        }
//    }
//}
//.Activities
//{
//    [Trigger(Category = "Tasks", Description = "Triggers when a user action is received.", Outcomes = new string[]
//      {
//        "x => x.state.actions"
//      })]
//public class CommitteeTask : Activity
//{
//    private readonly ICurrentUser _currentUser;
//    private readonly IMediator _mediator;
//    private readonly IInboxQueries _inboxQueries;
//    public CommitteeTask(ICurrentUser currentUser, IMediator mediator, IInboxQueries inboxQueries)
//    {
//        _currentUser = currentUser;
//        _mediator = mediator;
//        _inboxQueries = inboxQueries;
//    }
//    [ActivityInput(UIHint = ActivityInputUIHints.MultiText, Hint = "Provide a list of available actions", DefaultSyntax = "Json", SupportedSyntaxes = new string[]
//    {
//            "Json",
//            "JavaScript",
//            "Liquid"
//    }, OptionsProvider = typeof(ActionsProvider))]
//    public ICollection<string> Actions
//    {
//        get;
//        set;
//    }
//    [ActivityOutput]
//    public string Output
//    {
//        get;
//        set;
//    }
//    [ActivityInput(Name = "ViewName", UIHint = ActivityInputUIHints.SingleLine)]
//    public string ViewName { get; set; }



//    protected async override ValueTask<bool> OnCanExecuteAsync(ActivityExecutionContext context)
//    {
//        var bookmarkData = context.GetInput<TaskBookmark>();
//        if (context.WorkflowExecutionContext.IsFirstPass && bookmarkData == null)
//            return true;
//        var workflowInstance = context.WorkflowInstance;
//        var currentBlockingAction = workflowInstance.BlockingActivities.FirstOrDefault()?.ActivityId ?? null;
//        if (currentBlockingAction == null)
//        {
//            return false;
//        }
//        var inbox = await _inboxQueries.CheckAvtivityActionsForSpecificUser(workflowInstance.Id, currentBlockingAction.ToString(), _currentUser.Id);
//        if (inbox == null)
//        {
//            return false;
//        }
//        return true;
//    }
//    protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
//    {
//        return Suspend();
//    }
//    protected async override ValueTask<IActivityExecutionResult> OnResumeAsync(ActivityExecutionContext context)
//    {
//        var workflowInstance = context.WorkflowInstance;
//        await _mediator.Send(new UpdateInboxCommitteeCommand()
//        {
//            ActivityId = context.ActivityId,
//            WorkflowInstance = context.WorkflowInstance,
//            WorkflowBlueprint = context.WorkflowExecutionContext.WorkflowBlueprint,
//            ActionName = context.GetInput<TaskBookmark>().ActionName
//        });
//        var inbox = (await _inboxQueries.GetAllInboxEntriesByActvityId(workflowInstance.Id, context.ActivityId)).ToList();
//        if (inbox.Any(x => !x.ActionDate.HasValue))
//        {
//            return Suspend();
//        }
//        return Outcome(context.GetInput<TaskBookmark>().ActionName.ToString());
//    }
//}
//}
