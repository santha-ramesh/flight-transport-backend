namespace SkyRoute.Api.Shared.Helpers
{
    public static class BookingReferenceGenerator
    {
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string Generate()
        {
            var random = new Random();

            var code = new string(
                Enumerable.Range(0, 6)
                    .Select(_ => Characters[random.Next(Characters.Length)])
                    .ToArray());

            return $"SR-{code}";
        }
    }
}
