#define EXPORT_API __declspec(dllexport)

/*  real dll
#include <random>
#include <time.h>//*/

// Link following functions C-style (required for plugins)
extern "C"
{
	struct Vector3 {
		float x;
		float y;
		float z;
	};

	/*	real dll
	Vector3 lowerBound = { 1.f, 1.f, 1.f };
	Vector3 upperBound = { 2.f, 2.f, 2.f };//*/

	//checks and also resets rand seed
	void EXPORT_API SetScaleBounds(Vector3 lower, Vector3 upper) {
		/*	real dll
		if (lower.x > upper.x || lower.y > upper.y || lower.z > upper.z) {
			return;
		}
		lowerBound = lower;
		upperBound = {
			upper.x - lower.x,
			upper.y - lower.y,
			upper.z - lower.z
		};
		srand(time(0));//*/
	}

	//get a number
	Vector3 EXPORT_API FetchRandomScale() {
		/*	real dll
		return {
			lowerBound.x + (rand() % int(upperBound.x * 100.f)) * 0.001f,
			lowerBound.y + (rand() % int(upperBound.y * 100.f)) * 0.001f,
			lowerBound.z + (rand() % int(upperBound.z * 100.f)) * 0.001f,
		};//*/

		//  default/restore values
		return { 1, 1, 1 };
	}
}