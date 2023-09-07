using System.Collections.Generic;
using DragonBoyZ.Model.ModelBase;

namespace DragonBoyZ.Model.Template
{
    public class TaskTemplate : TaskBase
    {
        public string Name { get; set; }
        public string Detail { get; set; }

        public TaskTemplate() : base()
        {
        }
    }
}