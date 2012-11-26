/* This file has been generated from /home/uav/paparazzi/conf/messages.xml and /home/uav/paparazzi/conf/telemetry/dummy.xml */
/* Please DO NOT EDIT */

#ifndef _VAR_PERIODIC_H_
#define _VAR_PERIODIC_H_

#include "std.h"
#include "generated/airframe.h"


/* Macros for Main process */
#ifdef PERIODIC_C_MAIN
#ifndef TELEMETRY_MODE_MAIN
#define TELEMETRY_MODE_MAIN 0
#endif
uint8_t telemetry_mode_Main = TELEMETRY_MODE_MAIN;
#else /* PERIODIC_C_MAIN not defined (general header) */
extern uint8_t telemetry_mode_Main;
#endif /* PERIODIC_C_MAIN */
#define TELEMETRY_MODE_Main_default 0
#define PeriodicSendMain(_trans, _dev) {  /* 60Hz */ \
  if (telemetry_mode_Main == TELEMETRY_MODE_Main_default) {\
  }\
}

/* Macros for Ap process */
#ifdef PERIODIC_C_AP
#ifndef TELEMETRY_MODE_AP
#define TELEMETRY_MODE_AP 0
#endif
uint8_t telemetry_mode_Ap = TELEMETRY_MODE_AP;
#else /* PERIODIC_C_AP not defined (general header) */
extern uint8_t telemetry_mode_Ap;
#endif /* PERIODIC_C_AP */
#define TELEMETRY_MODE_Ap_default 0
#define PeriodicSendAp(_trans, _dev) {  /* 60Hz */ \
  if (telemetry_mode_Ap == TELEMETRY_MODE_Ap_default) {\
  }\
}

/* Macros for Fbw process */
#ifdef PERIODIC_C_FBW
#ifndef TELEMETRY_MODE_FBW
#define TELEMETRY_MODE_FBW 0
#endif
uint8_t telemetry_mode_Fbw = TELEMETRY_MODE_FBW;
#else /* PERIODIC_C_FBW not defined (general header) */
extern uint8_t telemetry_mode_Fbw;
#endif /* PERIODIC_C_FBW */
#define TELEMETRY_MODE_Fbw_default 0
#define PeriodicSendFbw(_trans, _dev) {  /* 60Hz */ \
  if (telemetry_mode_Fbw == TELEMETRY_MODE_Fbw_default) {\
  }\
}
#endif // _VAR_PERIODIC_H_
