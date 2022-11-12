
namespace ProjectEuler_11_LargestProductInGrid
{
    internal class InputOutput
    {
        /// <summary>
        /// Gets the project file path relative to the project deployment filesystem location.
        /// </summary>
        /// <returns>full project path as string</returns>
        internal static string GetProjectPath()
        {
            DirectoryInfo? projectPath = null;

            try
            {
                projectPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent!.Parent!.Parent;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return projectPath!.FullName;
        }

        /// <summary>
        /// Combines the passed file path segments into one full file path.
        /// </summary>
        /// <param name="segments"></param>
        /// <returns>combined file path as string</returns>
        internal static string CombineSegmentsToPath(string[] segments)
        {
            string path = string.Empty;

            try
            {
                path = Path.Combine(segments);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return path;
        }
    }
}
