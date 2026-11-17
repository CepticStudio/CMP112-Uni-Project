using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType { Slide, Rotate }

    [Header("Type")]
    [SerializeField] private DoorType doorType = DoorType.Slide;

    [Header("Slide Settings")]
    [Tooltip("Local-space offset the door moves to when open.")]
    [SerializeField] private Vector3 slideOffset = new Vector3(0f, 3f, 0f);

    [Header("Rotate Settings")]
    [Tooltip("Local euler angles added to the closed rotation when open.")]
    [SerializeField] private Vector3 rotateOffset = new Vector3(0f, 90f, 0f);

    [Header("Motion")]
    [SerializeField] private float speed = 2f;

    [Header("Audio (optional)")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    public bool IsOpen { get; private set; }

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Awake()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + slideOffset;

        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(rotateOffset);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        if (doorType == DoorType.Slide)
        {
            Vector3 target = IsOpen ? openPosition : closedPosition;
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, target, step * slideOffset.magnitude);
        }
        else
        {
            Quaternion target = IsOpen ? openRotation : closedRotation;
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation, target, step * rotateOffset.magnitude);
        }
    }

    public void Open()
    {
        if (IsOpen) return;
        IsOpen = true;
        PlaySound(openSound);
    }

    public void Close()
    {
        if (!IsOpen) return;
        IsOpen = false;
        PlaySound(closeSound);
    }

    public void Toggle()
    {
        if (IsOpen) Close();
        else Open();
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}
