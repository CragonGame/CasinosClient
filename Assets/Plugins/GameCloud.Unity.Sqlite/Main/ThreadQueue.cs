//using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class ThreadQueue //: MonoBehaviour 
{

	
	
	#region Public API
	
	public class TaskControl
	{
		bool			canceled = false;
		public bool		Canceled { get { return canceled; } }
		public void		Cancel() 
		{ 
			canceled = true; 
		}
		
		double			progress = 0.0;
		public double 	Progress {
			get { 
				double rv = 0.0;
				lock(this){ 
					rv = progress; 
				}
				return rv;
			}
			set {
				lock(this){
					progress = value;
				}
			}
		}
	}
	
	public delegate void WorkCallback (TaskControl control, object state);
	
	public static TaskControl QueueUserWorkItem( WorkCallback workproc, WaitCallback oncomplete, object state )
	{
		return ThreadQueue.Instance.AddUserTaskToQueue(workproc, oncomplete, state);
	}
	
	#endregion

	
	
	
	
    #region Singleton
	
	static ThreadQueue instance = null;
	static bool initialized = false;
	
	/*
	 *   the excuse for initialized variable because follow error happned on if(instance == false) at non main thread
	 * 
CompareBaseObjects  can only be called from the main thread.
Constructors and field initializers will be executed from the loading thread when loading a scene.
Don't use this function in the constructor or field initializers, instead move initialization code to the Awake or Start function.

	 * */
	
    static ThreadQueue Instance
    {
        get
        {
			if(initialized == false)
			{
                //GameObject obj = GameObject.Find("ThreadQueue");
                //if( obj == null )
                //{
                //    obj = new GameObject("ThreadQueue");
                //}
				
				// paranoia code :)
                //instance = obj.GetComponent<ThreadQueue>();
				
                //if(instance == null)
                //{
                //    instance = obj.AddComponent("ThreadQueue") as ThreadQueue;
                //}

                instance = new ThreadQueue();
				initialized = true;
			}
			
            return instance;
        }
    }
    #endregion

	
	#region Implementaion

	class Task
	{
		WorkCallback	workproc;
		WaitCallback	oncomplete;
		object 			state;
		TaskControl		control;
		
		public TaskControl	Control { get { return control;} }
		public WorkCallback Workproc { get { return workproc; } }
		public WaitCallback OnComplete { get { return oncomplete; } }
		public object 		State { get { return state; } }
		public bool 		Canceled { get { return control.Canceled; } }
		
		public Task(TaskControl	control, WorkCallback workproc, WaitCallback oncomplete, object state)
		{
			this.workproc = workproc;
			this.oncomplete = oncomplete;
			this.control = control;
			this.state = state;
		}
	}
	
	class WorkingThread
	{
		Thread thread = null;
		Semaphore sem = new Semaphore(1,999999);
		Queue<Task> work_queue = new Queue<Task>();
		Queue<Task> done_queue = new Queue<Task>();
		bool loop    = true;

		
		public bool Loop { get { return loop; } }
		
		
		public WorkingThread()
		{
			thread = new Thread(new ThreadStart(Run));
			thread.Start();
		}
		
		public void Terminate()
		{
			lock(work_queue)
			{
				work_queue.Clear();
				loop = false;
				sem.Release(1);
				thread = null;
			}
		}
		
		public void AddTask( Task task )
		{
			lock(work_queue)
			{
				work_queue.Enqueue(task);
			}
			sem.Release(1);
		}
		
		public void Update () 
		{
			bool trymore = true;
			while(trymore)
			{
				Task task = null;
				trymore = false;
				
				lock(done_queue)
				{
					while(done_queue.Count > 0)
					{
						trymore = true;
						task = done_queue.Dequeue();
						if(task.Canceled)
						{
							task = null;
							continue;
						}
						break;
					}
				}
				if(task!=null)
				{
					task.OnComplete(task.State);
				}
			}
		}
		
		void Run()
		{
			while(loop){
				sem.WaitOne(100);
				Task task = null;
				lock(work_queue)
				{
					while(work_queue.Count > 0)
					{
						task = work_queue.Dequeue();
						if(task.Canceled)
						{
							task = null;
							continue;
						}
						break;
					}
				}
				if(task!=null)
				{
					task.Workproc(task.Control,task.State);
					lock(done_queue)
					{
						done_queue.Enqueue(task);
					}
				}
			}
		}
	}
	
	
	WorkingThread thread = null;
	
	
	void Start() 
	{
		//DontDestroyOnLoad(gameObject);
		
		if( thread == null )
			thread = new WorkingThread();
	}

	
	void OnDestroy() 
	{
		if( thread != null )
			thread.Terminate();
		
		// clean up global value
		instance = null;
		initialized = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if( thread != null )
			thread.Update();
	}
	
	TaskControl AddUserTaskToQueue( WorkCallback workproc, WaitCallback oncomplete, object state )
	{
		TaskControl control = new TaskControl();
		
		if( thread == null )
			thread = new WorkingThread();

		thread.AddTask(new Task(control,workproc,oncomplete,state));
		
		return control;
	}
	

	#endregion

}
