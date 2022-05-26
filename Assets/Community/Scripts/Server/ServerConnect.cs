using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Storage;

public class ServerConnect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectServerDB(1); // 1: Osaka region DB Server
    }

    //현재는 1번 bucket만 존재하지만, 글로벌 서버가 생성되면 다른 버킷이 생길 수 있기 때문에
    public string GetServerStorageUrl(int bucketNum)
    {
        string bucketUrl;

        switch (bucketNum)
        {
            case 1:
                bucketUrl = "gs://soundspace-cf041.appspot.com/";
                Debug.Log("[Server-Storage] Connect to Osaka-general bucket: " + bucketUrl);
                break;

            //storage bucket 확장시 case 추가

            default:
                bucketUrl = "gs://soundspace-cf041.appspot.com/";
                Debug.Log("[Storage] Connect to Osaka-general bucket: " + bucketUrl);
                break;
        }

        return bucketUrl;
    }



    private void ConnectServerDB(int regionNum)
    {
        DatabaseReference reference;

        // Realtime DB 서버에 연결
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(GetServerDBUrl(regionNum));
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }



    //현재는 1번 오사카 region만 존재하지만, 글로벌 서버가 생성되면 다른 리전이 생길 수 있기 때문에
    public string GetServerDBUrl(int regionNum)
    {
        string regionUrl;

        switch (regionNum)
        {
            case 1: //오사카
                regionUrl = "https://soundspace-cf041.firebaseio.com/";
                Debug.Log("[Server-Database] Connect to Osaka region: " + regionUrl);
                break;

            //region 확장시 case 추가

            default: //오사카
                regionUrl = "https://soundspace-cf041.firebaseio.com/";
                Debug.Log("[Server-Database] Connect to Osaka region: " + regionUrl);
                break;
        }
        return regionUrl;
    }
}
