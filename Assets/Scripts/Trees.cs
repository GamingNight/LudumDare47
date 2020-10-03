using UnityEngine;

public class Trees : MonoBehaviour
{

    private Animator anim;
    private float randomnumber;

    void Start()
    {

        randomnumber = Random.Range(0.8f, 1.2f);

        anim = GetComponent<Animator>();
        anim.SetFloat("randomstart", randomnumber);
        anim.speed = randomnumber;

    }

}
