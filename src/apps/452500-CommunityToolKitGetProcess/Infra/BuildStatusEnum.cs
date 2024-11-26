using Ardalis.SmartEnum;

namespace CommunityToolKitGetProcess.Infra
{
    public sealed class BuildStatus : SmartEnum<BuildStatus>
    {
        public static readonly BuildStatus SolutionWithProjectsOpened_ButNoBuildEvenFiredYet 
            = new BuildStatus(nameof(SolutionWithProjectsOpened_ButNoBuildEvenFiredYet), 1,
                "Solution with Projects opened, but no Build Even fired yet");

        public static readonly BuildStatus NoSolutionWithProjectsCurrentlyOpened 
            = new BuildStatus(nameof(NoSolutionWithProjectsCurrentlyOpened), 2,
                "No Solution with Projects currently Opened");
        
        public static readonly BuildStatus BuildProjectConfigBegin 
            = new BuildStatus(nameof(BuildProjectConfigBegin), 3,
                "Build Project Config Begin");

        public static readonly BuildStatus BuildProjectConfigDone
            = new BuildStatus(nameof(BuildProjectConfigDone), 4,
                "Build Project Config Done");

        public static readonly BuildStatus BuildBegin
            = new BuildStatus(nameof(BuildBegin), 5,
                "Build Begin");

        public static readonly BuildStatus BuildDoneWithSuccess
            = new BuildStatus(nameof(BuildDoneWithSuccess), 6,
                "Build Done with Success");

        public static readonly BuildStatus BuildDoneWithFailure
            = new BuildStatus(nameof(BuildDoneWithFailure), 7,
                "Build Done with Failure");

        public string Description = string.Empty;

        private BuildStatus(string name, int value, string description = "") : base(name, value)
        {
            Description = description;
        }
    }
}
