using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpTextFX : MonoBehaviour
{
    private TextMeshPro mytext;

    [SerializeField] private float speed;
    [SerializeField] private float DisappearingSpeed;
    [SerializeField] private float colorDisappearingSpeed;

    [SerializeField] private float lifeTime;

    private float textTimer;

    private void Start()
    {
        mytext = GetComponent<TextMeshPro>();
        textTimer = lifeTime;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);

        textTimer = Time.time;

        if(textTimer < 0)
        {
            float alpha = mytext.color.a - colorDisappearingSpeed * Time.deltaTime;

            mytext.color = new Color(mytext.color.r, mytext.color.g, mytext.color.b, alpha);

            if(mytext.color.a < 50)
            {
                speed = DisappearingSpeed;
            }

            if(mytext.color.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
