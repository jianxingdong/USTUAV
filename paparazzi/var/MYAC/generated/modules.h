/* This file has been generated from /home/uav/USTUAV/paparazzi/conf/airframes/examples/LisaM2_Nov26_2012.xml */
/* Please DO NOT EDIT */

#ifndef MODULES_H
#define MODULES_H

#define MODULES_IDLE  0
#define MODULES_RUN   1
#define MODULES_START 2
#define MODULES_STOP  3

#ifdef MODULES_C
#define EXTERN_MODULES
#else
#define EXTERN_MODULES extern
#endif
#include "std.h"
#include "gps/gps_ubx_ucenter.h"
#include "core/sys_mon.h"
#include "adcs/adc_generic.h"

#define GPS_UBX_UCENTER_PERIODIC_PERIOD 0.250000
#define GPS_UBX_UCENTER_PERIODIC_FREQ 4.000000
#define PERIODIC_REPORT_SYSMON_PERIOD 1.000000
#define PERIODIC_REPORT_SYSMON_FREQ 1.000000
#define PERIODIC_SYSMON_PERIOD 0.016667
#define PERIODIC_SYSMON_FREQ 60.000000
#define ADC_GENERIC_PERIODIC_PERIOD 0.250000
#define ADC_GENERIC_PERIODIC_FREQ 4.000000

EXTERN_MODULES uint8_t gps_ubx_gps_ubx_ucenter_periodic_status;

#ifdef MODULES_C

static inline void modules_init(void) {
  gps_ubx_ucenter_init();
  gps_ubx_gps_ubx_ucenter_periodic_status = MODULES_START;
  init_sysmon();
  adc_generic_init();
}

static inline void modules_periodic_task(void) {
  static uint8_t i1; i1++; if (i1>=1) i1=0;
  static uint8_t i15; i15++; if (i15>=15) i15=0;
  static uint8_t i60; i60++; if (i60>=60) i60=0;

  if (gps_ubx_gps_ubx_ucenter_periodic_status == MODULES_START) { gps_ubx_ucenter_init(); gps_ubx_gps_ubx_ucenter_periodic_status = MODULES_RUN; }
  if (gps_ubx_gps_ubx_ucenter_periodic_status == MODULES_STOP) { ; gps_ubx_gps_ubx_ucenter_periodic_status = MODULES_IDLE; }



  periodic_sysmon();
  if (i15 == 0 && gps_ubx_gps_ubx_ucenter_periodic_status == MODULES_RUN) {
    gps_ubx_ucenter_periodic();
  }
  else if (i15 == 5) {
    adc_generic_periodic();
  }
  if (i60 == 10) {
    periodic_report_sysmon();
  }
}

static inline void modules_event_task(void) {
  event_sysmon();
}

#endif // MODULES_C

#ifdef MODULES_DATALINK_C

#include "messages.h"
#include "generated/airframe.h"
static inline void modules_parse_datalink(uint8_t msg_id __attribute__ ((unused))) {
}

#endif // MODULES_DATALINK_C

#endif // MODULES_H
