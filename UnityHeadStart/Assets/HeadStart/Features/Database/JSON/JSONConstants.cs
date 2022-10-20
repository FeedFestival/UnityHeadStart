namespace Assets.HeadStart.Features.Database.JSON
{
    public static class JSONConst
    {
        public static readonly DevicePlayer DEFAULT_PLAYER = new DevicePlayer()
        {
            localId = 0,
            name = "no-name-user",
            toiletPaper = 10,
            isFirstTime = true,
            playedTutorial = false,
            isRegistered = false,
            completedPercentage = 0
        };

        public static readonly GameSettings DEFAULT_SETTINGS = new GameSettings()
        {
            language = "en",
            isUsingSound = true,
            isCameraSetForThisDevice = false,
            cameraSize2D = 3,
            cameraSize3D = 3
        };

        public static readonly User DEFAULT_USER = new User()
        {
            Id = 0,
            LocalId = 0,
            Name = "no-name-user",
            UserType = UserType.CASUAL
        };

    }
}
