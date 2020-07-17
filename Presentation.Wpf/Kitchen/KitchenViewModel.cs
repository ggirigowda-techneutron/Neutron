using Presentation.Wpf.Utility;

namespace Presentation.Wpf.Kitchen
{
    /// <summary>
    ///     Kitchen View Model.
    /// </summary>
    public class KitchenViewModel : BaseObservable, IPageViewModel
    {
        /// <summary>
        ///     Name.
        /// </summary>
        public string Name => "Kitchen";
    }
}