import { Image, ImageBackground, KeyboardAvoidingView, StyleSheet, View } from 'react-native';
import React, { useState } from 'react';
import { GestureHandlerRootView, TextInput, TouchableOpacity } from 'react-native-gesture-handler';
import { Platform } from 'expo-modules-core';

function Logo() {
  return (
    <View className="flex-1 justify-center">
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
    <View className="flex-1 justify-center">
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
          className="flex-1 w-full"
          behavior={Platform.OS === 'ios' ? 'padding' : undefined}
          keyboardVerticalOffset={Platform.OS === 'ios' ? 0 : 50}
        >

          <TextInput
            placeholder="Email"
            placeholderTextColor={emailFocused ? '#fff' : '#000'}
            maxLength={250}
            keyboardType={'email-address'}
            className={emailFocused ? 'h-12 text-xl min-w-[250] max-w-[300] border-b-2 border-b-white' : 'h-10 text-xl min-w-[250] max-w-[300] border-b-2 border-b-black'}
            onFocus={() => setEmailFocused(true)}
            onBlur={() => setEmailFocused(false)}
          />

          <TextInput
            placeholder="password"
            placeholderTextColor={emailFocused ? '#fff' : '#000'}
            keyboardType={'visible-password'}
            secureTextEntry={true}
            inputMode="none"
            maxLength={250}
            className={passwordFocused ? 'h-12 text-xl min-w-[250] max-w-[300] border-b-2 border-b-white' : 'h-10 text-xl min-w-[250] max-w-[300] border-b-2 border-b-black'}
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
      className="flex-1 w-full"
    >
      <ImageBackground source={backgroundImage} resizeMode="cover" className="flex-1 justify-center items-center pt-10">
        <Logo />
        <GoogleLoginButton />

        <EmailAndPasswordForm />
      </ImageBackground>
    </View>
  );
}

