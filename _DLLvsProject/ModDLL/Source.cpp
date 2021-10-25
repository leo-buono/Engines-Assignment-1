#define EXPORT_API __declspec(dllexport)

//real dll
#include <random>
#include <time.h>

// Link following functions C-style (required for plugins)
extern "C"
{
	struct Vector3 {
		float x;
		float y;
		float z;
	};
	
	float EXPORT_API getMaxHP()
	{
		srand(time(0));

		return 1 + rand() % 25;
	}

	float EXPORT_API getSpeed()
	{
		return 1 + rand() % 20;
	}

	float EXPORT_API getDamage()
	{
		return 1 + rand() % 20;
	}

	float EXPORT_API getHPup()
	{
		return 1 + rand() % 10;
	}

	float EXPORT_API getEnemyDamage()
	{
		return 1 + rand() % 25;
	}

	float EXPORT_API getEnemySpeed()
	{
		return 1 + rand() % 20;
	}

	float EXPORT_API getEnemyHP()
	{
		return 1 + rand() % 20;
	}
	
}