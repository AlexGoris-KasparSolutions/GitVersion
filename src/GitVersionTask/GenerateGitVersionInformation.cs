namespace GitVersionTask
{
    public class GenerateGitVersionInformation
    {
        // This method is entrypoint for the task declared in .props file
        public static Output Execute(Input input) => GitVersionTasks.GenerateGitVersionInformation(input);

        public sealed class Input : InputWithCommonAdditionalProperties
        {
        }

        public sealed class Output
        {
            public string GitVersionInformationFilePath { get; set; }
        }
    }
}
