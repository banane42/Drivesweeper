public static class PlayerInfo {

    public static bool canFire;

    public static int health;
    public static int exp;
    public static float fireRate;
    public static int ammo;
    public static int flags;

    public static int stoneGoal = 250;//default 500
    public static int blackPowederGoal = 100;//default 175
    public static int rubyGoal = 25;//default 50
    public static int emeraldGoal = 75;

    public static float droneMineTime;
    public static int droneCount;
    public static int maxDrones;
    public static float droneSpeed;

    public static float stone = 0;
    public static float blackMinePowder = 0;
    public static float rubies = 0;
    public static float emeralds = 0;

    public enum ControlMode {
        Shooting, Build
    }
    public static ControlMode controlMode;

    public enum SecondaryShootingMode {
        Null, Drone, Flag
    }
    public static SecondaryShootingMode secondaryShootingMode;

    public enum BuildingMode {
        Null, Unselected, Shop, Miner
    }
    public static BuildingMode buildingMode;

    public static void SetDefault() {

        canFire = true;

        health = 100;
        exp = 0;
        fireRate = 0.5f;
        ammo = 250;
        flags = 5;

        droneMineTime = 5f;
        droneCount = 0;
        maxDrones = 3;
        droneSpeed = 2f;

        //stone = 0f;
        //blackMinePowder = 0f;

    }

    public static void SetDev() {

        canFire = true;

        health = 1000;
        exp = 0;
        fireRate = 0.01f;
        ammo = 25000;
        flags = 25000;

        droneMineTime = 0.5f;
        droneCount = 0;
        maxDrones = 100;
        droneSpeed = 10f;

        //stone = 100000000f;
        //blackMinePowder = 100000000f;

    }
}
