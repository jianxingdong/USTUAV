/* This file has been generated from /home/uav/USTUAV/paparazzi/conf/airframes/examples/LisaM2_Nov26_2012.xml */
/* Please DO NOT EDIT */

#ifndef AIRFRAME_H
#define AIRFRAME_H

#define AIRFRAME_NAME "MYAC"
#define AC_ID 1
#define MD5SUM ((uint8_t*)"\105\233\156\044\264\363\243\360\010\332\243\335\201\014\272\262")

#define SERVOS_NB 6

#define SERVO_THROTTLE 1
#define SERVO_THROTTLE_NEUTRAL 1120
#define SERVO_THROTTLE_TRAVEL_UP 0.0833333333333
#define SERVO_THROTTLE_TRAVEL_DOWN 0
#define SERVO_THROTTLE_MAX 1920
#define SERVO_THROTTLE_MIN 1120

#define SERVO_ELEVATOR 2
#define SERVO_ELEVATOR_NEUTRAL 1515
#define SERVO_ELEVATOR_TRAVEL_UP 0.0401041666667
#define SERVO_ELEVATOR_TRAVEL_DOWN 0.0432291666667
#define SERVO_ELEVATOR_MAX 1900
#define SERVO_ELEVATOR_MIN 1100

#define SERVO_RUDDER 3
#define SERVO_RUDDER_NEUTRAL 1440
#define SERVO_RUDDER_TRAVEL_UP 0.0635416666667
#define SERVO_RUDDER_TRAVEL_DOWN 0.0510416666667
#define SERVO_RUDDER_MAX 2050
#define SERVO_RUDDER_MIN 950

#define SERVO_AILERON_RIGHT 4
#define SERVO_AILERON_RIGHT_NEUTRAL 1500
#define SERVO_AILERON_RIGHT_TRAVEL_UP 0.0520833333333
#define SERVO_AILERON_RIGHT_TRAVEL_DOWN 0.0520833333333
#define SERVO_AILERON_RIGHT_MAX 2000
#define SERVO_AILERON_RIGHT_MIN 1000

#define SERVO_AILERON_LEFT 5
#define SERVO_AILERON_LEFT_NEUTRAL 1500
#define SERVO_AILERON_LEFT_TRAVEL_UP 0.0520833333333
#define SERVO_AILERON_LEFT_TRAVEL_DOWN 0.0520833333333
#define SERVO_AILERON_LEFT_MAX 2000
#define SERVO_AILERON_LEFT_MIN 1000


#define COMMAND_THROTTLE 0
#define COMMAND_ROLL 1
#define COMMAND_PITCH 2
#define COMMAND_YAW 3
#define COMMANDS_NB 4
#define COMMANDS_FAILSAFE {0,0,0,2000}


#define SetCommandsFromRC(_commands_array, _rc_array) { \
  _commands_array[COMMAND_ROLL] = _rc_array[RADIO_ROLL];\
  _commands_array[COMMAND_PITCH] = _rc_array[RADIO_PITCH];\
  _commands_array[COMMAND_THROTTLE] = _rc_array[RADIO_THROTTLE];\
  _commands_array[COMMAND_YAW] = _rc_array[RADIO_YAW];\
}

#define SECTION_MIXER 1
#define COMBI_SWITCH 0.3

#define SetActuatorsFromCommands(values) { \
  uint32_t servo_value;\
  float command_value;\
  command_value = values[COMMAND_THROTTLE];\
  command_value *= command_value>0 ? SERVO_THROTTLE_TRAVEL_UP : SERVO_THROTTLE_TRAVEL_DOWN;\
  servo_value = SERVO_THROTTLE_NEUTRAL + (int32_t)(command_value);\
  actuators[SERVO_THROTTLE] = ChopServo(servo_value, SERVO_THROTTLE_MIN, SERVO_THROTTLE_MAX);\
\
  Actuator(SERVO_THROTTLE) = SERVOS_TICS_OF_USEC(actuators[SERVO_THROTTLE]);\
\
  command_value = values[COMMAND_PITCH];\
  command_value *= command_value>0 ? SERVO_ELEVATOR_TRAVEL_UP : SERVO_ELEVATOR_TRAVEL_DOWN;\
  servo_value = SERVO_ELEVATOR_NEUTRAL + (int32_t)(command_value);\
  actuators[SERVO_ELEVATOR] = ChopServo(servo_value, SERVO_ELEVATOR_MIN, SERVO_ELEVATOR_MAX);\
\
  Actuator(SERVO_ELEVATOR) = SERVOS_TICS_OF_USEC(actuators[SERVO_ELEVATOR]);\
\
  command_value = values[COMMAND_YAW] + values[COMMAND_ROLL]*COMBI_SWITCH;\
  command_value *= command_value>0 ? SERVO_RUDDER_TRAVEL_UP : SERVO_RUDDER_TRAVEL_DOWN;\
  servo_value = SERVO_RUDDER_NEUTRAL + (int32_t)(command_value);\
  actuators[SERVO_RUDDER] = ChopServo(servo_value, SERVO_RUDDER_MIN, SERVO_RUDDER_MAX);\
\
  Actuator(SERVO_RUDDER) = SERVOS_TICS_OF_USEC(actuators[SERVO_RUDDER]);\
\
  command_value = values[COMMAND_ROLL];\
  command_value *= command_value>0 ? SERVO_AILERON_LEFT_TRAVEL_UP : SERVO_AILERON_LEFT_TRAVEL_DOWN;\
  servo_value = SERVO_AILERON_LEFT_NEUTRAL + (int32_t)(command_value);\
  actuators[SERVO_AILERON_LEFT] = ChopServo(servo_value, SERVO_AILERON_LEFT_MIN, SERVO_AILERON_LEFT_MAX);\
\
  Actuator(SERVO_AILERON_LEFT) = SERVOS_TICS_OF_USEC(actuators[SERVO_AILERON_LEFT]);\
\
  command_value = values[COMMAND_ROLL];\
  command_value *= command_value>0 ? SERVO_AILERON_RIGHT_TRAVEL_UP : SERVO_AILERON_RIGHT_TRAVEL_DOWN;\
  servo_value = SERVO_AILERON_RIGHT_NEUTRAL + (int32_t)(command_value);\
  actuators[SERVO_AILERON_RIGHT] = ChopServo(servo_value, SERVO_AILERON_RIGHT_MIN, SERVO_AILERON_RIGHT_MAX);\
\
  Actuator(SERVO_AILERON_RIGHT) = SERVOS_TICS_OF_USEC(actuators[SERVO_AILERON_RIGHT]);\
\
  ActuatorsCommit();\
}

#define AllActuatorsInit() { \
  ActuatorsInit();\
}

#define SECTION_AUTO1 1
#define AUTO1_MAX_ROLL 0.872664625
#define AUTO1_MAX_PITCH 0.6981317

#define SECTION_IMU 1
#define IMU_BODY_TO_IMU_PHI 0.
#define IMU_BODY_TO_IMU_THETA 0.
#define IMU_BODY_TO_IMU_PSI 0.

#define SECTION_INS 1
#define INS_ROLL_NEUTRAL_DEFAULT 0.
#define INS_PITCH_NEUTRAL_DEFAULT 0.

#define SECTION_BAT 1
#define MILLIAMP_AT_FULL_THROTTLE 20000
#define CATASTROPHIC_BAT_LEVEL 0.0
#define CRITIC_BAT_LEVEL 0.0
#define LOW_BAT_LEVEL 0.0
#define MAX_BAT_LEVEL 12.5

#define SECTION_MISC 1
#define NOMINAL_AIRSPEED 15.
#define CARROT 4.
#define KILL_MODE_DISTANCE (2.0*MAX_DIST_FROM_HOME)
#define CONTROL_RATE 60
#define ALT_KALMAN_ENABLED TRUE
#define DEFAULT_CIRCLE_RADIUS 100.

#define SECTION_VERTICAL CONTROL 1
#define V_CTL_POWER_CTL_BAT_NOMINAL 11.0
#define V_CTL_ALTITUDE_PGAIN 0.075
#define V_CTL_ALTITUDE_MAX_CLIMB 4.
#define V_CTL_AUTO_THROTTLE_NOMINAL_CRUISE_THROTTLE 0.5
#define V_CTL_AUTO_THROTTLE_MIN_CRUISE_THROTTLE 0.2
#define V_CTL_AUTO_THROTTLE_MAX_CRUISE_THROTTLE 1.0
#define V_CTL_AUTO_THROTTLE_LOITER_TRIM 1500
#define V_CTL_AUTO_THROTTLE_DASH_TRIM -1000
#define V_CTL_AUTO_THROTTLE_CLIMB_THROTTLE_INCREMENT 0.1
#define V_CTL_AUTO_THROTTLE_PGAIN 0.02
#define V_CTL_AUTO_THROTTLE_IGAIN 0.03
#define V_CTL_AUTO_THROTTLE_PITCH_OF_VZ_PGAIN 0.05
#define V_CTL_AUTO_PITCH_PGAIN 0.125
#define V_CTL_AUTO_PITCH_IGAIN 0.025
#define V_CTL_AUTO_PITCH_MAX_PITCH 0.4363323125
#define V_CTL_AUTO_PITCH_MIN_PITCH -0.4363323125
#define V_CTL_THROTTLE_SLEW 1.0

#define SECTION_HORIZONTAL CONTROL 1
#define H_CTL_COURSE_PGAIN 1.0
#define H_CTL_COURSE_DGAIN 0.4
#define H_CTL_ROLL_MAX_SETPOINT 0.6108652375
#define H_CTL_PITCH_MAX_SETPOINT 0.4363323125
#define H_CTL_PITCH_MIN_SETPOINT -0.4363323125
#define H_CTL_PITCH_PGAIN 20000.
#define H_CTL_PITCH_DGAIN 1.5
#define H_CTL_ELEVATOR_OF_ROLL 2500
#define H_CTL_ROLL_ATTITUDE_GAIN 7400
#define H_CTL_ROLL_RATE_GAIN 200

#define SECTION_FAILSAFE 1
#define FAILSAFE_DELAY_WITHOUT_GPS 3
#define FAILSAFE_DEFAULT_THROTTLE 0.4
#define FAILSAFE_DEFAULT_ROLL 0.2617993875
#define FAILSAFE_DEFAULT_PITCH 0.
#define FAILSAFE_HOME_RADIUS 90


#endif // AIRFRAME_H
