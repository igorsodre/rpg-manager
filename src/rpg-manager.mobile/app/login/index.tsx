import {Image, ImageBackground, StyleSheet, Text, View} from "react-native";
import React from "react";
import {Gesture, GestureDetector, GestureHandlerRootView} from "react-native-gesture-handler";

function Logo() {
    return (
        <View style={{flex: 1, justifyContent: 'center'}}>
            <Image resizeMode='contain'
                   source={require('../../assets/images/AppLogo.png')}
                   style={{width: 300, height: 70}}/>
        </View>
    );
}

const googleLoginHandler = Gesture.Tap().numberOfTaps(1).onEnd(() => {
    console.log('Google login button pressed');
});

function FacebookLoginButton() {
    return <View style={{flex: 1, justifyContent: 'center'}}>
        <GestureHandlerRootView >
            <GestureDetector  gesture={googleLoginHandler} >
                <Image resizeMode='contain'
                       source={require('../../assets/images/SignupSignInWithGoogleButton.png')}
                       style={{width: 250, height: 60}}/>
            </GestureDetector>
        </GestureHandlerRootView>
    </View>;
}

function EmailAndPasswordForm() {
    return (
        <View style={{flex: 3}}>
            <Text>
                Testing for now
            </Text>
        </View>
    )
}

const backgroundImage = require('../../assets/images/LoginBackgrownd.png')
export default function LoginPage() {
    return (
        <View style={styles.container}>
            <ImageBackground source={backgroundImage} resizeMode='cover' style={styles.background}>
                <Logo/>
                <FacebookLoginButton/>

                <EmailAndPasswordForm/>
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
    }
})
