namespace Jal.Validator.Interface
{
    public interface ITransientValidator
    {
        dynamic Context { get; set; }

        string Group { get; set; }

        string Subgroup { get; set; }
    }
}
