using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneGridManager : MonoBehaviour {
	
	public float squareSide;
	public int minX, maxX, minY, maxY;
	public int[,] CostToTraverse;
	public GameObject playerReference;
	public int COST_PLAYER = 5;
	public int COST_REGULAR_ZOMBIE = 5;
	public int COST_WALL = 100000;
	public int COST_GRAVESTONE = 100000;
	
	internal struct GridData
	{
		public int fromX, fromY;
		public int toX, toY;
		public float xOffset, yOffset;
		public int cost;
	};
	
	private int xBias = 0, yBias = 0;
	private int gridMaxX, gridMaxY;
	private IDictionary<int, GridData> objectToGridData = new Dictionary<int, GridData>();
	// Use this for initialization
	void Start ()
	{
		if (minX < 0) {
			xBias = 1 - minX;
		}
		if (minY < 0) {
			yBias = 1 - minY;
		}
		
		// Convert coordinates to positives
		gridMaxX = maxX + xBias;
		gridMaxY = maxY + yBias;
		Debug.Log(string.Format("bias: {0}, {1}; max: {2}, {3}", xBias, yBias, gridMaxX, gridMaxY));
		CostToTraverse = new int[gridMaxX, gridMaxY];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void getGridCoords(float worldX, float worldY, out int gridX, out int gridY)
	{
		gridX = (int)(worldX + xBias);
		gridY = (int)(worldY + yBias);
	}
	
	private void updateGrid(int fromX, int fromY, int toX, int toY, int cost)
	{
		fromX = Mathf.Max(fromX, 0);
		fromY = Mathf.Max(fromY, 0);
		toX =  Mathf.Min(toX, gridMaxX - 1);
		toY = Mathf.Min(toY, gridMaxY - 1);
		for (int i = fromX; i <= toX; i++)
		{
			for (int j = fromY; j <= toY; j++)
			{
				CostToTraverse[i, j] += cost;
			}
		}
	}
	
	public bool isCellValid(int x, int y)
	{
		if (x < 0 || y < 0 || x >= gridMaxX || y >= gridMaxY)
		{
			return true;
		}
		return false;
	}
	
	public void registerObject(GameObject obj, float x, float y, float width, float height, int cost)
	{
		GridData d = new GridData();
		d.cost = cost;
		d.xOffset = obj.transform.position.x - x;
		d.yOffset = obj.transform.position.y - y;
		getGridCoords(x - width/2, y - height/2, out d.fromX, out d.fromY);
		getGridCoords(x + width/2, y + height/2, out d.toX, out d.toY);
		objectToGridData[obj.GetHashCode()] = d;
		this.updateGrid(d.fromX, d.fromY, d.toX, d.toY, cost);
		Debug.Log(string.Format("Registered object: {0} at ({1},{2}) ({3}, {4})", obj.GetHashCode(), d.fromX, d.fromY, d.toX, d.toY));
	}
	
	public void registerPlayer(GameObject obj, float x, float y, float width, float height, int cost)
	{
		playerReference = obj;
		registerObject(obj, x, y, width, height, cost);
	}
	
	public void updateObjectPosition(GameObject obj)
	{
		GridData data = objectToGridData[obj.GetHashCode()];
		
		// Remove old weight from old positions
		this.updateGrid(data.fromX, data.fromY, data.toX, data.toY, -data.cost);
		
		// calculate new positions
		var width = data.toX - data.fromX;
		var height = data.toY - data.fromY;
		getGridCoords(obj.transform.position.x + data.xOffset - width/2, obj.transform.position.y + data.yOffset - height/2, out data.fromX, out data.fromY);
		getGridCoords(obj.transform.position.x + data.xOffset + width/2, obj.transform.position.y + data.yOffset + height/2, out data.toX, out data.toY);
		
		// update map
		objectToGridData[obj.GetHashCode()] = data;
		
		// Add weight to new positions
		this.updateGrid(data.fromX, data.fromY, data.toX, data.toY, -data.cost);
		
		//Debug.Log(string.Format("{0} at ({1},{2}) ({3}, {4})", obj.GetHashCode(), data.fromX, data.fromY, data.toX, data.toY));
	}
	
	public void removeObject(GameObject obj)
	{
		GridData data = objectToGridData[obj.GetHashCode()];
		this.updateGrid(data.fromX, data.fromY, data.toX, data.toY, -data.cost);
		objectToGridData.Remove(obj.GetHashCode());
		//Debug.Log(string.Format("{0} at ({1},{2}) ({3}, {4})", obj.GetHashCode(), data.fromX, data.fromY, data.toX, data.toY));
	}
}
