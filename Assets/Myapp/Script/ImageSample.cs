using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageSample : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager _imageManager;
    // Start is called before the first frame update
    void Start()
    {
        _imageManager = GetComponent<ARTrackedImageManager>();
        _imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            AddARObject(trackedImage);
        }
    }
    private void AddARObject(ARTrackedImage trackedImage)
    {
        switch (trackedImage.trackingState)
        {
            case TrackingState.Tracking:
                Debug.Log(trackedImage.referenceImage.name);
                Debug.Log(trackedImage.gameObject.transform);
                break;
            case TrackingState.None:
                break;
            case TrackingState.Limited:
                break;
        }

    }

}
