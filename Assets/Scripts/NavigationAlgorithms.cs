using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NavigationAlgorithms : MonoBehaviour {
	
	public bool test = false;
	
	public struct Cell
	{ public int x,y; }
	
	private SceneGridManager gridManager;
	private Cell[] neighbourCells = new Cell[8];
	// Use this for initialization
	void Start () {
		gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<SceneGridManager>();
		//private Cell[] moves = {{1, 1}, {1, 0}, {1, -1}, {0, -1}, {0, 1}, {-1, -1}, {-1, 0}, {-1, 1}};
		neighbourCells[0] = new Cell { x = 1, y = 1};
		neighbourCells[1] = new Cell { x = 1, y = 0};
		neighbourCells[2] = new Cell { x = 1, y = -1};
		neighbourCells[3] = new Cell { x = 0, y = 1};
		neighbourCells[4] = new Cell { x = 0, y = -1};
		neighbourCells[5] = new Cell { x = -1, y = 1};
		neighbourCells[6] = new Cell { x = -1, y = 0};
		neighbourCells[7] = new Cell { x = -1, y = -1};
		
	}
	
	public Vector2 normalizedOptimalMove(float startX, float startY, float endX, float endY)
	{	
		LinkedList<Cell> optimal_path = astar (startX, startY, endX, endY);
		if (optimal_path == null)
		{
			return new Vector2(0, 0);
		}
		
		if (optimal_path.Count == 0) return new Vector2(0, 0);
		
		Vector2 realCoords = gridManager.getRealCoords(optimal_path.ElementAt(1).x, optimal_path.ElementAt(1).y);
		//int gx, gy;
		//gridManager.getGridCoords(startX, startY, out gx, out gy);
		Vector2 approxStart = new Vector2(startX, startY);
		Debug.Log(string.Format("Next step coords: {0},{1}", realCoords.x, realCoords.y));
		return (realCoords - approxStart).normalized;
	}
	
	public LinkedList<Cell> astar(float startX, float startY, float endX, float endY)
	{
		Debug.Log(string.Format("A*: {0},{1} to {2},{3}", startX, startY, endX, endY));
		Cell start = new Cell();
		Cell end = new Cell();
		gridManager.getGridCoords(startX, startY, out start.x, out start.y);
		gridManager.getGridCoords(endX, endY, out end.x, out end.y);
		
		HashSet<Cell> visited = new HashSet<Cell>();
		LinkedList<Cell> pending = new LinkedList<Cell>();
		Dictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
		Dictionary<Cell, float> g_score = new Dictionary<Cell, float>();
		Dictionary<Cell, float> f_score = new Dictionary<Cell, float>();
		
		pending.AddLast(start);
		g_score[start] = gridManager.CostToTraverse[start.x, start.y];
		f_score[start] = g_score[start] + estimate(start, endX, endY);
		int i = 0;
		while (pending.Count > 0)
		{
			var current = f_score.Where(kvp => pending.Contains(kvp.Key)).Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
			if (i > 100) Debug.Log(string.Format("Current ({0}) : {1}, {2}", i, current.x, current.y));
			if (i > 130) return null;
			i++;
			if (current.x == end.x && current.y == end.y)
			{
				return reconstructPath(cameFrom, current);
			}
			
			pending.Remove(current);
			visited.Add(current);
			
			foreach (var dir in neighbourCells)
			{
				Cell neighbour = new Cell();
				neighbour.x = dir.x + current.x;
				neighbour.y = dir.y + current.y;
				if (!gridManager.isCellValid(neighbour.x, neighbour.y))
				{
					continue;
				}
				
				//Debug.Log(string.Format("neighbour: {0}, {1}", neighbour.x, neighbour.y));
				if (visited.Contains(neighbour))
				{
					continue;
				}
				
				var tentative_g_score = g_score[current] + gridManager.CostToTraverse[neighbour.x, neighbour.y];
				if (!pending.Contains(neighbour))
				{
					pending.AddLast(neighbour);
				}
				else if (tentative_g_score >= g_score[neighbour])
				{
					continue;
				}
				
				cameFrom[neighbour] = current;
				g_score[neighbour] = tentative_g_score;
				f_score[neighbour] = tentative_g_score + estimate(neighbour, endX, endY);
			}		
		}
		return null;
	}
	
	public LinkedList<Cell> reconstructPath(Dictionary<Cell, Cell> cameFrom, Cell end)
	{
		LinkedList<Cell> path = new LinkedList<Cell>();
		path.AddLast(end);
		
		while (cameFrom.ContainsKey(end))
		{
			end = cameFrom[end];
			path.AddFirst(end);
		}
		
		return path;
	}
	
	public float estimate(Cell from, float endX, float endY)
	{
		var a = gridManager.getRealCoords(from.x, from.y);
		var b = new Vector2(endX, endY);
		return Vector2.Distance(a, b);
	}
	
	void Update()
	{
		if (!test) return;
		test = false;
		
		LinkedList<Cell> r = astar(48.2f, -10.1f, 0, 0);
		if (r == null)
		{
			Debug.Log ("kor");
				for (int j = 169; j < 178; j++)
					Debug.Log (string.Format ("[{0}] = {1}", j, gridManager.CostToTraverse[j,25]));
		}
		int i = 0;
		foreach (var c in r)
		{
			Debug.Log(string.Format ("{0}: {1}, {2}", i, c.x, c.y));
		}
	}
}
