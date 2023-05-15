import { AppProps } from "next/app";
import { NavMenu } from "../components";

import "./globals.css";
import "bootstrap/dist/css/bootstrap.css";

export default function MyApp({ Component, pageProps }: AppProps) {
  return (
    <NavMenu>
      <main className="container">
        <Component {...pageProps} />
      </main>
    </NavMenu>
  );
}
