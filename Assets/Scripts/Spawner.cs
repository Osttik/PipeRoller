using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject StartPipe;
    public GameObject EndPipe;
    public GameObject RolledPipe;
    public GameObject DirectPipe;
    public GameObject PipeField;

    private DijkstraAlgorithm _djikstra;
    private int _height;
    private int _lenght;
    private float _step;
    private Data _data;

    private void Start()
    {
        _data = GameObject.Find("Data").GetComponent<Data>();
        _height = _data.Height;
        _lenght = _data.Lenght;

        _step = 1.28f;
        _djikstra = transform.GetComponent<DijkstraAlgorithm>();
        Spawn();
    }

    public void Spawn()
    {
        CleanPipes();
        GeneratePipes();
    }

    private void Pp(bool[,] arr)
    {
        string str = "";
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                str += arr[i, j] + " ";
            }
            str += "\n";
        }
        print(str);
    }
    private void GeneratePipes()
    {
        bool[,] field = new bool[_height, _lenght];
        List<string> path = _djikstra.BuildRandomPath(_height, _lenght);

        Vector2 startPosition = new Vector2((_lenght - 1) * _step / 2 * -1, (_height - 1) * _step / 2);
        Vector2 position;
        Quaternion rotation = new Quaternion();

        position = InstatiateBeginAndEnd(path, startPosition, ref rotation);

        field[0, 0] = true;
        field[_height - 1, _lenght - 1] = true;

        InstatiatePath(field, path, ref position, ref rotation);
        Pp(field);
        position = FillEmptyWithPipes(field, position, rotation);
    }

    private Vector2 FillEmptyWithPipes(bool[,] field, Vector2 position, Quaternion rotation)
    {
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _lenght; j++)
            {
                if (!field[i, j])
                {
                    int randomObject = UnityEngine.Random.Range(0, 1);

                    position = new Vector2((i - (_height - 1) / 2) * _step, (j - (_lenght - 1) / 2) * _step * -1);

                    int randomRotation = UnityEngine.Random.Range(0, 4);
                    rotation.eulerAngles = new Vector3(0, 0, randomRotation * 90);

                    if (randomObject == 1)
                    {
                        Instantiate(RolledPipe, position, rotation, PipeField.transform);
                    }
                    else
                    {
                        Instantiate(DirectPipe, position, rotation, PipeField.transform);
                    }
                }
            }
        }

        return position;
    }

    private void InstatiatePath(bool[,] field, List<string> path, ref Vector2 position, ref Quaternion rotation)
    {
        int[] prevXY = ParseToXY(path[path.Count - 2]);

        int[] prePrevXY = new int[2] { _height - 1, _lenght - 1 };

        field[prevXY[0], prevXY[1]] = true;

        for (int i = path.Count - 3; i >= 0; i--)
        {
            int[] currentXY = ParseToXY(path[i]);
            position = new Vector2((prevXY[0] - (_height - 1) / 2) * _step, (prevXY[1] - (_lenght - 1) / 2) * _step * -1);

            int randomRotation = UnityEngine.Random.Range(0, 4);
            rotation.eulerAngles = new Vector3(0, 0, randomRotation * 90);

            if (prePrevXY[0] == currentXY[0] || prePrevXY[1] == currentXY[1])
            {
                Instantiate(DirectPipe, position, rotation, PipeField.transform);
            }
            else
            {
                Instantiate(RolledPipe, position, rotation, PipeField.transform);
            }

            prePrevXY = prevXY;
            prevXY = currentXY;

            field[currentXY[0], currentXY[1]] = true;
        }
    }

    private Vector2 InstatiateBeginAndEnd(List<string> path, Vector2 startPosition, ref Quaternion rotation)
    {
        Vector2 position;
        int[] startRotation = ParseToXY(path[1]);
        if (startRotation[0] == 0)
            rotation.eulerAngles = new Vector3(0, 0, -90f);
        else
            rotation.eulerAngles = new Vector3(0, 0, 0);

        Instantiate(StartPipe, startPosition, rotation, PipeField.transform);

        int[] rotationCoordination = ParseToXY(path[path.Count - 2]);

        if (rotationCoordination[1] == _height - 1)
            rotation.eulerAngles = new Vector3(0, 0, 180);
        else
            rotation.eulerAngles = new Vector3(0, 0, 90);

        position = new Vector2(startPosition.x * -1, startPosition.y * -1);

        Instantiate(EndPipe, position, rotation, PipeField.transform);
        return position;
    }

    private int[] ParseToXY(string str)
    {
        string[] parsedStr = str.Split(' ');

        int x = Convert.ToInt32(parsedStr[0]);
        int y = Convert.ToInt32(parsedStr[1]);

        int[] parsedArrayOfXY = new int[] { x, y };

        return parsedArrayOfXY;
    }

    private void CleanPipes()
    {
        int childCount = PipeField.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Destroy(PipeField.transform.GetChild(i).gameObject);
        }
    }
}
