# PlatformObjectPooler
This is an object pool spawner I created for Midnight Zone that I would like the share with the game dev community.
It's designed to choose a random platform (or gameobject) from the pool and set it active to a specified spawn position.  

**IMPORTANT**
The system relies on the spawned object to handle setting itself to inactive after its use.  If all objects from spawner are set to active it will instantiate another pool for it to select a random inactive gameobject from.  There is no limit to this, so overtime it will turn into an issue if not handled properly.

When implimenting this script you will need to:
-create a new gameobject in the hierarchy and name it ObjectPoolSpawner
-create a new gameobject in the hierarchy and name it SpawnPosition
-Place the spawn position where desired
-attach PlatformPooler script
-Click ObjectPoolSpawner gameobject and drag SpawnPosition gameobject into appropriate place in inspector
-set the spawn rate
-Open up the Pool list and add as many different objects you would like to have randomly spawned into your scene
-tag is optional (its there if you want to elaborate more on the script)
-set prefab you would like pooled
-set the size of the pool for object
Size is somewhat dynamic, if you only have one object and that object is active- more objects will be instantiated until there are objects that are inactive that the spawner can choose from to activate.
