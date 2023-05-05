#include "utils.h";
#include <string>

namespace hack {
	
	
	bool set_speed(float speed) {
		__int64 unity_player = reinterpret_cast<uint64_t>(GetModuleHandleA("unityplayer.dll"));
		try {
			utils::write<float>(utils::read<__int64>(unity_player + 0x1D21D78) + 0xFC, speed);
			return true;
		}
		catch(__int64 unity_player){
			return false;
		}
	}

	float get_speed() {
		try {
			__int64 unity_player = reinterpret_cast<uint64_t>(GetModuleHandleA("unityplayer.dll"));
			__int64 test = utils::read<__int64>(unity_player + 0x1D21D78) + 0xFC;
			std::cout << "Speed?: " + std::to_string(test) << std::endl;
			return (float)test;
		}
		catch (__int64 unity_player) {
			return 1.f;
		}
	}
}