import { Image, ImageBackground, StyleSheet, Text, View } from 'react-native';
import React, { useState } from 'react';
import { Gesture, GestureHandlerRootView, TextInput, TouchableOpacity } from 'react-native-gesture-handler';

function Logo() {
  return (
    <View style={{ flex: 1, justifyContent: 'center' }}>
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
    <View style={{ flex: 1, justifyContent: 'center' }}>
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
  const [isFocused, setIsFocused] = useState(false);

  return (
    <View style={{ flex: 3 }}>
      <GestureHandlerRootView>
        <TextInput
          placeholder="Email"
          style={isFocused ? styles.emailTextInputFocused : styles.emailTextInput}
          onFocus={() => setIsFocused(true)}
          onBlur={() => setIsFocused(false)}
        />
      </GestureHandlerRootView>
    </View>
  );
}

const backgroundImage = require('../../assets/images/LoginBackgrownd.png');
export default function Index() {
  return (
    <View style={styles.container}>
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
  },
  background: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  emailTextInput: {
    height: 50,
    fontSize: 18,
    borderBottomWidth: 2,
    borderBottomColor: '#000',
  },
  emailTextInputFocused: {
    height: 50,
    fontSize: 18,
    borderBottomWidth: 2, // Make the underline more pronounced
    borderBottomColor: '#fff', // Change the underline color to white
  },
});
