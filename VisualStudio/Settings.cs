using ModSettings;

namespace IndoorsGreenery 
{
    internal class Settings : JsonModSettings
    {
        internal static Settings instance = new Settings();

        [Section("Difficulty settings")]

        [Name("Gardener")]
        [Description("Start with the basics for gardening. Adds craftable versions of tools, fertilizer, distilled water and soil. Default = No")]
        public bool gardener = false;

        [Name("No Plant Nutrients")]
        [Description("Prevents non-craftable plant nutrients to spawn. Needs game restart. Default = No")]
        public bool noPlantNutrients = false;

        [Name("No 'Rain Drops'")]
        [Description("Prevents non-craftable distilled water to spawn. Needs game restart. Default = No")]
        public bool noRainDrops = false;

        [Name("No Gardening Tools")]
        [Description("Prevents non-craftable gardening tools to spawn. Needs game restart. Default = No")]
        public bool noGardeningTools = false;

        [Name("No Gardening Mesh")]
        [Description("Prevents non-craftable gardening mesh to spawn. Needs game restart. Default = No")]
        public bool noGardeningMesh = false;

        [Name("Disable Custom Plants")]
        [Description("Prevents custom plants seeds to spawn. Needs game restart. Default = No")]
        public bool noSeeds = false;
    }
}
