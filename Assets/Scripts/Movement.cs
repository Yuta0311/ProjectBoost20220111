using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine; //オーディオの入力欄を作る

    [SerializeField] ParticleSystem mainEngineParticles;//エンジンのイベントアニメーションの素材入力欄を作成
    [SerializeField] ParticleSystem leftThrusterParticles;//エンジンのイベントアニメーションの素材入力欄を作成
    [SerializeField] ParticleSystem rightThrusterParticles;//エンジンのイベントアニメーションの素材入力欄を作成

    Rigidbody rb;
    AudioSource audioSource; //定義している

    bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>(); //オーディオを使えるようにしている
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else //スペースを押していない時にオーディオが停止
        {
            StopThrusting();

        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rotateleft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotateright();
        }
        else //AもDも押さなかった場合
        {
            StopRotating();
        }
    }

    //ここからロジック定義？？みたいな感じーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー

    void StartThrusting()
    {
        //Debug.Log("Pressed Space - Thrusting");
        //rb.AddRelativeForce(0, 1, 0); //上に上がる
        //rb.AddRelativeForce(Vector3.up); //上に上がる（上の数字のコードと同じ意味を成す＝省略形みたいな）
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) //音が連続して再生するためにIF文
        {
            //audioSource.Play();//スペースを押している時にオーディオが再生
            audioSource.PlayOneShot(mainEngine); //オーディオの入力欄を作る　Play()とPlayOneShot()の違いは音が重複できるかどうか、後者ができる
        }

        if (!mainEngineParticles.isPlaying)//アニメーションが連続して再生するためにIF文
        {
            mainEngineParticles.Play(); //エンジンのアニメーションを再生
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();//スペースを押していない時にオーディオが停止
        mainEngineParticles.Stop(); //スペースを押していない時にエンジンのアニメーションを停止
    }

    void Rotateleft()
    {
        //Transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        ApplyRoation(rotationThrust);//一番上で入力欄からの定義をここに反映

        if (!mainEngineParticles.isPlaying)//アニメーションが連続して再生するためにIF文
        {
            rightThrusterParticles.Play(); //エンジンのアニメーションを再生
        }
    }

    void Rotateright()
    {
        //transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);//ーをつけて反転？できるようにしている
        ApplyRoation(-rotationThrust);

        if (!mainEngineParticles.isPlaying)//アニメーションが連続して再生するためにIF文
        {
            leftThrusterParticles.Play(); //エンジンのアニメーションを再生
        }
    }

    void StopRotating()
    {
        rightThrusterParticles.Stop(); //エンジンのアニメーションを再生
        leftThrusterParticles.Stop(); //エンジンのアニメーションを再生
    }

    void ApplyRoation(float rotationThisFrame)//ここで上の定義に対して、代入している、、、見直そう
    {
        rb.freezeRotation = true; //物理的な制御をしている（変な動きを止める的な？）
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;//物理的な制御をしている（変な動きを止める的な？）
    }
}
