using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float impulseFactor;
    public float forceFactor;
    public Vector3 initBallPos;
    List<Vector3> kegelsPos;
    List<Quaternion> kegelsRotations;

    private bool isThrown = false;
    // Start is called before the first frame update
    void Start()
    {
        kegelsPos = new List<Vector3>();
        kegelsRotations = new List<Quaternion>();
        initBallPos = GameObject.FindGameObjectWithTag("Ball").transform.position;
        var kegels = GameObject.FindGameObjectsWithTag("Kegel");
        foreach(var keg in kegels)
        {
            kegelsPos.Add(keg.transform.position);
            kegelsRotations.Add(keg.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowBall();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            ResetBall();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            
            ResetGame();
            return;
        }
    }

    void MoveRight()
    {
        if (!isThrown)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * impulseFactor, ForceMode.Impulse);
        }
    }

    void MoveLeft()
    {
        if (!isThrown)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * impulseFactor, ForceMode.Impulse);
        }
    }

    void ThrowBall()
    {
        if (!isThrown)
        {
            isThrown = true;
            GetComponent<Rigidbody>().AddForce(Vector3.forward * forceFactor);
        }
    }

    void ResetBall()
    {
        this.gameObject.GetComponent<Rigidbody>().position = initBallPos;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        isThrown = false;
    }

    void ResetGame()
    {
        ResetBall();
        var kegels = GameObject.FindGameObjectsWithTag("Kegel");
        int i = 0;
        foreach(var keg in kegels)
        {
            var kegPhysics = keg.GetComponent<Rigidbody>();
            kegPhysics.velocity = Vector3.zero;
            kegPhysics.angularVelocity = Vector3.zero;
            kegPhysics.position = kegelsPos[i];
            kegPhysics.rotation = kegelsRotations[i];
            i++;
        }
        
    }
}
