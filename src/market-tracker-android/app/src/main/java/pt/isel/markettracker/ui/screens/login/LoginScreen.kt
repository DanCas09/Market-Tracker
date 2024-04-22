package pt.isel.markettracker.ui.screens.login

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Email
import androidx.compose.material.icons.filled.Password
import androidx.compose.material3.Button
import androidx.compose.material3.Icon
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.testTag
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.example.markettracker.R
import pt.isel.markettracker.ui.components.buttons.ButtonWithImage
import pt.isel.markettracker.ui.components.text.LinesWithElementCentered
import pt.isel.markettracker.ui.components.text.MarketTrackerTextField
import pt.isel.markettracker.ui.theme.mainFont

const val LoginScreenTag = "LoginScreenTag"
const val LoginEmailInputTag = "LoginEmailInputTag"
const val LoginPasswordInputTag = "LoginPasswordInputTag"

@Composable
fun LoginScreen(
    onSignUpRequested: () -> Unit,
    loginScreenViewModel: LoginScreenViewModel
) {
    Box(
        modifier = Modifier
            .fillMaxSize()
            .testTag(LoginScreenTag),
        contentAlignment = Alignment.Center
    ) {
        Column(
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.spacedBy(24.dp)
        ) {
            Text(
                text = "Login",
                fontFamily = mainFont,
                color = Color.Red,
                fontSize = 24.sp
            )

            MarketTrackerTextField(
                value = loginScreenViewModel.email,
                onValueChange = { loginScreenViewModel.email = it },
                leadingIcon = {
                    Icon(
                        Icons.Default.Email,
                        contentDescription = "Email"
                    )
                },
                placeholder = {
                    Text(text = "Email", fontFamily = mainFont)
                },
                modifier = Modifier.testTag(LoginEmailInputTag)
            )

            MarketTrackerTextField(
                value = loginScreenViewModel.password,
                onValueChange = { loginScreenViewModel.password = it },
                leadingIcon = {
                    Icon(
                        imageVector = Icons.Default.Password,
                        contentDescription = "password"
                    )
                },
                placeholder = {
                    Text(text = "Password", fontFamily = mainFont)
                },
                isPassword = true,
                modifier = Modifier.testTag(LoginPasswordInputTag)
            )

            Button(
                onClick = loginScreenViewModel::login
            ) {
                Text(text = "Login", fontFamily = mainFont)
            }

            LinesWithElementCentered(
                xOffset = 3,
                color = Color.LightGray
            ) {
                Text(
                    text = "ou",
                    modifier = Modifier.weight(0.2f),
                    textAlign = TextAlign.Center,
                    fontFamily = mainFont
                )
            }

            GoogleLoginButton(
                onGoogleLoginRequested = loginScreenViewModel::handleGoogleSignInTask
            )

            ButtonWithImage(
                onClick = onSignUpRequested,
                image = R.drawable.mt_logo,
                buttonText = "Criar conta Market Tracker"
            )
        }
    }
}
