/* This file has been generated from /home/uav/paparazzi/var/setup_lisam2/settings_modules.xml /home/uav/paparazzi/conf/settings/test_fixedwing_actuators.xml /home/uav/paparazzi/var/setup_lisam2/settings_telemetry.xml */
/* Please DO NOT EDIT */

#ifndef SETTINGS_H
#define SETTINGS_H

#define RCSettings(mode_changed) { \
}

#include "firmwares/fixedwing/actuators.h"
#include "generated/modules.h"
#include "generated/periodic_telemetry.h"

#define DlSetting(_idx, _value) { \
  switch (_idx) { \
    case 0: actuators[0] = _value; break;\
    case 1: actuators[1] = _value; break;\
    case 2: actuators[2] = _value; break;\
    case 3: actuators[3] = _value; break;\
    case 4: actuators[4] = _value; break;\
    case 5: actuators[5] = _value; break;\
    case 6: actuators[6] = _value; break;\
    case 7: actuators[7] = _value; break;\
    default: break;\
  }\
}
#define PeriodicSendDlValue(_trans, _dev) { \
  static uint8_t i;\
  float var;\
  if (i >= 8) i = 0;\
  switch (i) { \
    case 0: var = actuators[0]; break;\
    case 1: var = actuators[1]; break;\
    case 2: var = actuators[2]; break;\
    case 3: var = actuators[3]; break;\
    case 4: var = actuators[4]; break;\
    case 5: var = actuators[5]; break;\
    case 6: var = actuators[6]; break;\
    case 7: var = actuators[7]; break;\
    default: var = 0.; break;\
  }\
  DOWNLINK_SEND_DL_VALUE(_trans, _dev, &i, &var);\
  i++;\
}
static inline float settings_get_value(uint8_t i) {
  switch (i) { \
    case 0: return actuators[0];
    case 1: return actuators[1];
    case 2: return actuators[2];
    case 3: return actuators[3];
    case 4: return actuators[4];
    case 5: return actuators[5];
    case 6: return actuators[6];
    case 7: return actuators[7];
    default: return 0.;
    }
  }

/* Persistent Settings */
struct PersistentSettings {
};

extern struct PersistentSettings pers_settings;

static inline void persistent_settings_store( void ) {
}

static inline void persistent_settings_load( void ) {
};

#endif // SETTINGS_H
