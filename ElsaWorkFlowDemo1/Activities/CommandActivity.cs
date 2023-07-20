using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Design;

using Elsa.Services;
using Elsa.Services.Models;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Mapster.Adapters;

namespace ElsaWorkFlowDemo1.Activities
{
    [Trigger(Category = "Tasks", Description = "Triggers to execute a command and advance the workflow if command is succeeded")]
    public class CommandActivity : Activity
    {
        public CommandActivity(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [ActivityInput(UIHint = ActivityInputUIHints.Dropdown, Hint = "Command to be executed"  )]
        public string Command
        {
            get;
            set;
        }
        [ActivityInput(UIHint = ActivityInputUIHints.Dropdown, Hint = "The Next Status" )]
        public string NextStatus
        {
            get;
            set;
        }

        [ActivityInput(UIHint = ActivityInputUIHints.MultiLine, Hint = "Requested Body", SupportedSyntaxes = new string[]
        {

            "JavaScript",
            "Json"

        }, DefaultSyntax = "JavaScript")]
        public string Payload
        {
            get;
            set;
        }

        private void AddRange(IDictionary<string, object?> destination, Dictionary<string, object> source)
        {
            if (source != default && source.Count() > 0)
            {
                foreach (var item in source)
                {
                    if (destination.ContainsKey(item.Key.ToLower()))
                        destination[item.Key.ToLower()] = item.Value;
                    else
                        destination.Add(item.Key.ToLower(), item.Value);
                }
            }
        }
    }
}
