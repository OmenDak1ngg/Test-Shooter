public class Pistol : Gun
{
    protected override void OnEnable()
    {
        base.OnEnable();

        UserInput.ShootKeyReleased += OnShootKeyReleased;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        UserInput.ShootKeyReleased += OnShootKeyReleased;
    }

    protected override void Shoot()
    {
        if (CanShoot == false)
            return;

        base.Shoot();

        CanShoot = false;
    }

    private void OnShootKeyReleased()
    {
        CanShoot = true;
    }
}