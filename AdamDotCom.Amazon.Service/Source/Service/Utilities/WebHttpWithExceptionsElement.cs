using System;
using System.ServiceModel.Configuration;

namespace AdamDotCom.Amazon.Service.Utilities
{
    public class WebHttpWithExceptionsElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof (WebHttpWithExceptions); }
        }

        protected override object CreateBehavior()
        {
            return new WebHttpWithExceptions();
        }
    }
}