
using System;
using UnityEngine;
using UnityEngine.SceneManagement;//ここで「SceneManagement」を使うという定義をする

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoaddelaty = 2f;//遅らせる時間の入力欄を作成
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticle; //当たった時のイベントアニメーションの素材入力欄を作成
    [SerializeField] ParticleSystem crashParticle; //当たった時のイベントアニメーションの素材入力欄を作成

    AudioSource audioSource; //定義している

    bool isTransitioning = false; //「bool」の説明「https://note.com/want_drive_pc/n/n79f3f0f81d6a」//ここではゲームが衝突後反応できなくようになる
    bool colliionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //オーディオを使えるようにしている
    }

    void Update()
    {
        RespondToDebugkeys(); //開発者用に次のレベルにすぐ行けるように
    }

    void RespondToDebugkeys() //開発者用に次のレベルにすぐ行けるように定義
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();

        }
        else if (Input.GetKey(KeyCode.C))
        {
            colliionDisabled = !colliionDisabled;//Cを押すと衝突が無効になる
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //if (isTransitioning) //isTransitioningがtrueの時に、ゲームを操作しないようにしている？
        if (isTransitioning || colliionDisabled) //「colliionDisabled」を追加して管理者用にCを押すと衝突が無効になる
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;

            case "Finish":
                //Debug.Log("You Finished!");　次のレベル的な
                //LoadNextLevel();//次のレベル（下で定義）を呼び寄せてる
                StartSuccessSequence();
                break;

            case "Fuel":
                Debug.Log("燃料が追加されました");
                break;

            default:
                //Debug.Log("ゲームオーバー");
                //ReloadLevel(); //ゲームオーバー（下で定義）を呼び寄せてる
                //Invoke("ReloadLevel" , 1f); //ゲームオーバーの時に少し時間（反応）を遅らせる
                StartCrashSequence();
                break;

        }
    }

    void StartSuccessSequence()//当たった時の遅延（次のレベル）
    {
        isTransitioning = true;

        audioSource.Stop(); //オーディオを再生ストップ
        audioSource.PlayOneShot(success);//上記の入力欄で入れたオーディオを再生できるようにしている

        successParticle.Play();　//当たった時にイベントアニメーションを再生

        GetComponent<Movement>().enabled = false; //Movement.csを呼び出し？
        Invoke("LoadNextLevel", levelLoaddelaty); //遅らせる時間「Invoke」
    }

    void StartCrashSequence() //当たった時の遅延（ゲームオーバー）
    {
        isTransitioning = true;

        audioSource.Stop();　//オーディオを再生ストップ
        audioSource.PlayOneShot(crash);//上記の入力欄で入れたオーディオを再生できるようにしている

        crashParticle.Play(); //当たった時にイベントアニメーションを再生

        GetComponent<Movement>().enabled = false; //Movement.csを呼び出し？
        //Invoke("ReloadLevel", 1f);　→　下で定義している「ReloadLevel()」を呼び出しているのと1秒遅延させている
        Invoke("ReloadLevel", levelLoaddelaty); //遅らせる時間「Invoke」
    }

    void LoadNextLevel() //Finishタグのコンテンツに当たると次のレベルに行くことを定義
    {
        //一番上で「SceneManagement」を定義しないといけない　　using UnityEngine.SceneManagement;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;//これが何か上と同じ0？数字になる（数字が増えたときの自動化的な？）
        int nextSceneIndex = currentSceneIndex + 1;//レベルn+1を定義
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) //上で定義した（レベルn+1）が設定しているシーンと同じ数になった時
        {
            nextSceneIndex = 0;
        }
        //SceneManager.LoadScene(currentSceneIndex + 1); //ここで+1にすることで次のレベルに行くことになる
        SceneManager.LoadScene(nextSceneIndex); //次のステージに行くか、最初に戻るか
    }

    void ReloadLevel() //ゲームオーバーを定義（最初に戻る的な）
    {
        //一番上で「SceneManagement」を定義しないといけない　　using UnityEngine.SceneManagement;
        //SceneManager.LoadScene(0);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //これが何か上と同じ0？数字になる（数字が増えたときの自動化的な？）
        SceneManager.LoadScene(currentSceneIndex);
    }
}
