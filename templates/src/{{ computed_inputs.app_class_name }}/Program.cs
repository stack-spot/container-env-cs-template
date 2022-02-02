using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace {{ computed_inputs.app_class_name }}
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new {{ computed_inputs.app_class_name }}Stack(app, "{{ computed_inputs.app_class_name }}Stack", new StackProps
            {
                Env = new Amazon.CDK.Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("AWS_TARGET_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("AWS_REGION"),
                }
            });
            app.Synth();
        }
    }
}
