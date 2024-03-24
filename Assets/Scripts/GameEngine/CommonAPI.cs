public static class CommonAPI
{
    public const string RotationDirection = nameof(RotationDirection);
    public const string Dispatcher = nameof(Dispatcher);
}

public static class MovementAPI
{
    public const string Direction = nameof(Direction);
    public const string IsMoving = nameof(IsMoving);
}

public static class AttackAPI
{
    public const string WeaponsStorage = nameof(WeaponsStorage);
    public const string FireAction = nameof(FireAction);
    public const string SwitchToNextWeaponAction = nameof(SwitchToNextWeaponAction);
    public const string CurrentWeapon = nameof(CurrentWeapon);
}

public static class LifeAPI
{
    public const string IsAlive = nameof(IsAlive);
    public const string TakeDamageAction = nameof(TakeDamageAction);
}

public static class WeaponAPI
{
    public const string Config = nameof(Config);
}

public static class TeamAPI
{
    public const string Team = nameof(Team);
    public const string IsEnemy = nameof(IsEnemy);
}
