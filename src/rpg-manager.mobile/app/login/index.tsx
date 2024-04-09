import {ImageBackground, StyleSheet, Text, View} from "react-native";
import React from "react";

function Logo() {
    return null;
}

function FacebookLoginButton() {
    return null;
}

function EmailAndPasswordForm() {
    return (
        <Text>
            Testing for now
        </Text>
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
        justifyContent: 'center',
    }
})
