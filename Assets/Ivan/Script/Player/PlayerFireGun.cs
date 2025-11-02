using UnityEngine;
using UnityEngine.Audio;

public class PlayerFireGun : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform SpawnPointBullet;
    [SerializeField] private GameObject bulletPrefab;

    [Tooltip("La fuente de audio que reproducirá el sonido del disparo.")]
    [SerializeField] private AudioClip fireSoundClip;

    [Tooltip("El Audio Mixer Group (bus) al que se enviará el sonido del disparo.")]
    [SerializeField] private AudioMixerGroup audioGroupFire;

    private AudioSource m_AudioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
        if (m_AudioSource == null)
        {
            m_AudioSource = gameObject.AddComponent<AudioSource>();
        }

        if (audioGroupFire != null)
        {
            m_AudioSource.outputAudioMixerGroup = audioGroupFire;
        }
    }

    public void FireBullet()
    {

        Vector3 BulletDir = SpawnPointBullet.right;
        Quaternion rot = Quaternion.LookRotation(BulletDir, Vector3.up);
        GameObject Bullet = Instantiate(bulletPrefab, SpawnPointBullet.position, rot);

        if (Bullet.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = BulletDir * 20f;
        }

        if (m_AudioSource != null && fireSoundClip != null)
        {
            m_AudioSource.PlayOneShot(fireSoundClip);
        }
    }


    public void IsGunFireAttackTrue()
    {
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("FireGun");
    }
}