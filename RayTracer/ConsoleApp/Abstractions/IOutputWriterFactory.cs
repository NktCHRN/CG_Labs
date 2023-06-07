namespace ConsoleApp.Abstractions;
public interface IOutputWriterFactory
{
    IOutputWriter CreateOutputWriter(string outputName);
}
