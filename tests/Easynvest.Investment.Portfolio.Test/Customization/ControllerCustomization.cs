using AutoFixture;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Easynvest.Investment.Portfolio.Test.Customization
{
    public class ControllerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register<ICompositeMetadataDetailsProvider>(() => new CustomCompositeMetadataDetailsProvider());
            fixture.Inject(new ViewDataDictionary(fixture.Create<DefaultModelMetadataProvider>(), fixture.Create<ModelStateDictionary>()));
        }

        private class CustomCompositeMetadataDetailsProvider : ICompositeMetadataDetailsProvider
        {
            public void CreateBindingMetadata(BindingMetadataProviderContext context)
            {
            }

            public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
            {
            }

            public void CreateValidationMetadata(ValidationMetadataProviderContext context)
            {
            }
        }
    }
}