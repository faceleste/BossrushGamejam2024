using System.Collections;
public interface IBoss
{
    void BossBleed();
    void BossFire();

    void OnCollisionEnter2D();
    void FixedUpdate();
    IEnumerator Rotine();
    IEnumerator TimeToDie();
    IEnumerator SwitchColor();
    IEnumerator DelayToShakeCam();

}