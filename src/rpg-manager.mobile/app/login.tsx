import { Image, ImageBackground, KeyboardAvoidingView, StyleSheet, Text, View } from 'react-native';
import React, { useState } from 'react';
import { Gesture, GestureHandlerRootView, TextInput, TouchableOpacity } from 'react-native-gesture-handler';
import { Platform } from 'expo-modules-core';
import { useHeaderHeight } from '@react-navigation/elements';

function Logo() {
  return (
    <View style={{ flex: 1, justifyContent: 'center' }}>
      <Image resizeMode="contain"
             source={require('../assets/images/AppLogo.png')}
             style={{ width: 300, height: 70 }} />
    </View>
  );
}


function GoogleLoginButton() {
  const handlePress = () => {
    console.log('Google login button pressed');
  };

  return (
    <View style={{ flex: 1, justifyContent: 'center' }}>
      <GestureHandlerRootView>
        <TouchableOpacity onPress={handlePress}>
          <Image resizeMode="contain"
                 source={require('../assets/images/SignupSignInWithGoogleButton.png')}
                 style={{ width: 250, height: 60 }} />
        </TouchableOpacity>
      </GestureHandlerRootView>
    </View>
  );
}

function EmailAndPasswordForm() {
  const [emailFocused, setEmailFocused] = useState(false);
  const [passwordFocused, setPasswordFocused] = useState(false);

  return (
    <View style={{ flex: 3 }}>
      <GestureHandlerRootView>
        <KeyboardAvoidingView
          style={{ flex: 1, width: '100%' }}
          behavior={Platform.OS === 'ios' ? 'padding' : undefined}
          keyboardVerticalOffset={Platform.OS === 'ios' ? 0 : 50}
        >

          <TextInput
            placeholder="Email"
            maxLength={250}
            keyboardType={'email-address'}
            style={emailFocused ? styles.textInputFocused : styles.textInput}
            onFocus={() => setEmailFocused(true)}
            onBlur={() => setEmailFocused(false)}
          />

          <TextInput
            placeholder="password"
            keyboardType={'visible-password'}
            secureTextEntry={true}
            inputMode="none"
            maxLength={250}
            style={passwordFocused ? styles.textInputFocused : styles.textInput}
            onFocus={() => setPasswordFocused(true)}
            onBlur={() => setPasswordFocused(false)}
          />
        </KeyboardAvoidingView>
      </GestureHandlerRootView>
    </View>
  );
}

const backgroundImage = require('../assets/images/LoginBackgrownd.png');
export default function Login() {
  return (
    <View
      style={styles.container}
    >
      <ImageBackground source={backgroundImage} resizeMode="cover" style={styles.background}>
        <Logo />
        <GoogleLoginButton />

        <EmailAndPasswordForm />
      </ImageBackground>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    width: '100%',
  },
  background: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    paddingTop: 47,
  },
  textInput: {
    height: 50,
    fontSize: 18,
    borderBottomWidth: 2,
    borderBottomColor: '#000',
  },
  textInputFocused: {
    height: 50,
    fontSize: 18,
    minWidth: 250,
    maxWidth: 300,
    borderBottomWidth: 2, // Make the underline more pronounced
    borderBottomColor: '#fff', // Change the underline color to white
  },
});
