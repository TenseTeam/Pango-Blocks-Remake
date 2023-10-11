namespace VUDK.Features.Main.Inputs.MobileInputs.MobileInputActions.Interfaces
{
    public interface ICastMobileInput<T> where T : InputTouchBase
    {
        public T InputTouch { get; }
    }
}
