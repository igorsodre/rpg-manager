import { Image, ImageBackground, KeyboardAvoidingView, StyleSheet, Text, View } from 'react-native';
import React, { useState } from 'react';
import { GestureHandlerRootView, TextInput, TouchableOpacity } from 'react-native-gesture-handler';
import { Platform } from 'expo-modules-core';

function Logo() {
  return (
    <View className="flex-1 justify-center">
      <Image resizeMode="contain"
             source={require('../../assets/images/AppLogo.png')}
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
                 source={require('../../assets/images/SignupSignInWithGoogleButton.png')}
                 style={{ width: 250, height: 60 }} />
        </TouchableOpacity>
      </GestureHandlerRootView>
    </View>
  );
}

function EmailAndPasswordForm() {
  const [emailFocused, setEmailFocused] = useState(false);
  const [passwordFocused, setPasswordFocused] = useState(false);

  let handlePress = () => {
    console.log('EmailAndPasswordForm button pressed');
  };
  return (
    <View className="flex-[3]">
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
            placeholder="Password"
            placeholderTextColor={emailFocused ? '#fff' : '#000'}
            keyboardType={'visible-password'}
            secureTextEntry={true}
            inputMode="none"
            maxLength={250}
            className={passwordFocused ? 'h-12 text-xl min-w-[250] max-w-[300] border-b-2 border-b-white' : 'h-10 text-xl min-w-[250] max-w-[300] border-b-2 border-b-black'}
            onFocus={() => setPasswordFocused(true)}
            onBlur={() => setPasswordFocused(false)}
          />

          <TouchableOpacity onPress={handlePress} className="h-14 pt-2 items-end">
            <View className="flex-1 justify-center items-center py-2 px-4 bg-teal-500  h-12 w-1/2">
              <Text className="text-black font-bold text-xl">Login</Text>
            </View>
          </TouchableOpacity>

        </KeyboardAvoidingView>
      </GestureHandlerRootView>
    </View>
  );
}

export default function Login() {
  return (
    <View
      className="flex-1 w-full"
    >
      <ImageBackground
        source={require('../../assets/images/LoginBackgrownd.png')}
        resizeMode="cover"
        className="flex-1 justify-center items-center pt-10">
        <Logo />
        <GoogleLoginButton />

        <EmailAndPasswordForm />
      </ImageBackground>
    </View>
  );
}

