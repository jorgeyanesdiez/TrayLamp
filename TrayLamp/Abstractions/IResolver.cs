namespace TrayLamp.Abstractions
{
    public interface IResolver<in TInput, out TOutput>
    {
        public TOutput Resolve(TInput input);
    }
}
