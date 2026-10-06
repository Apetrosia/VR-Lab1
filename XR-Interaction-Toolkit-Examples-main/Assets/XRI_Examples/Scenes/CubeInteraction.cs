using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CubeInteraction : MonoBehaviour
{
    public Material[] materials;
    public AudioClip grabSound;
    public Animator anim;
    public float throwForce = 5f;

    private XRGrabInteractable grabInteractable;
    private AudioSource audioSource;
    private Renderer rend;
    private int currentMatIndex = 0;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        anim = GetComponent<Animator>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs e)
    {
        currentMatIndex = (currentMatIndex + 1) % materials.Length;
        rend.material = materials[currentMatIndex];

        if (grabSound != null)
            audioSource.PlayOneShot(grabSound);
    }

    void OnRelease(SelectExitEventArgs e)
    {
        anim.SetBool("IsHeld", !anim.GetBool("IsHeld"));
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * throwForce, ForceMode.Impulse);
    }

    void Update()
    {
        if (grabInteractable.isSelected)
        {
            transform.Rotate(Vector3.up * 100f * Time.deltaTime);
        }
    }
}