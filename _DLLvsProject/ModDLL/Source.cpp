#define EXPORT_API __declspec(dllexport)

//real dll
#include <random>
#include <time.h>

// Link following functions C-style (required for plugins)
extern "C"
{
	struct Color {
		float r;
		float g;
		float b;
		float a;
	};
	
	//stats dll
	/*
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
	//*/
	
	//HP bar dll
	float EXPORT_API GetBarWidth(float health, float maxHealth, float currentWidth, float maxWidth, float dt) {
		//direct
		//return health / maxHealth * maxWidth;
		//smoothed motion
		return health / maxHealth * maxWidth * dt + (1 - dt) * currentWidth;
	}

	Color EXPORT_API GetColour(float percentage) {
		//Makes it red
		//return Color{1.f, 0.f, 0.f, 1.f};
		//percentage based
		return Color{ 1 - percentage, percentage, 0.f, 1.f};
	}
}