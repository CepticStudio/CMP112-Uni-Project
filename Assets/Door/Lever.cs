using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

 
[RequireComponent(typeof(Collider))]
public class Lever : MonoBehaviour
{
    public enum LeverMode { OpenOnly, Toggle }

    [Header("Linked Doors")]
    [SerializeField] private Door[] doors;

    [Header("Behaviour")]
    [SerializeField] private LeverMode mode = LeverMode.Toggle;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private string playerTag = "Player";

    [Header("Handle Animation")]
    [Tooltip("The part of the lever that moves. Leave empty to skip animation.")]
    [SerializeField] private Transform handle;
    [SerializeField] private Vector3 pulledRotation = new Vector3(-60f, 0f, 0f);
    [SerializeField] private float pullDuration = 0.35f;

    [Header("Events")]
    public UnityEvent onPulled;
    public UnityEvent onReset;

    [Header("Audio (optional)")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pullSound;

    public bool IsPulled { get; private set; }

    private bool playerInRange;
    private bool isAnimating;
    private Quaternion startRotation;
    private Quaternion endRotation;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;

        if (handle != null)
        {
            startRotation = handle.localRotation;
            endRotation = startRotation * Quaternion.Euler(pulledRotation);
        }
    }

    private void Update()
    {
        if (playerInRange && !isAnimating && Input.GetKeyDown(interactKey))
            Pull();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag)) playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag)) playerInRange = false;
    }

    /// <summary>Can also be called from other scripts, buttons, raycasts, etc.</summary>
    public void Pull()
    {
        if (isAnimating) return;

        // Open-only levers can't be pulled again.
        if (mode == LeverMode.OpenOnly && IsPulled) return;

        IsPulled = !IsPulled;

        if (audioSource != null && pullSound != null)
            audioSource.PlayOneShot(pullSound);

        foreach (Door door in doors)
        {
            if (door == null) continue;

            if (IsPulled) door.Open();
            else door.Close();
        }

        if (IsPulled) onPulled.Invoke();
        else onReset.Invoke();

        if (handle != null)
            StartCoroutine(AnimateHandle(IsPulled ? endRotation : startRotation));
    }

    private IEnumerator AnimateHandle(Quaternion target)
    {
        isAnimating = true;
        Quaternion from = handle.localRotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / Mathf.Max(pullDuration, 0.01f);
            handle.localRotation = Quaternion.Slerp(from, target, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        handle.localRotation = target;
        isAnimating = false;
    }
}
