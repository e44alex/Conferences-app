import React, { useCallback } from "react";
import "bootstrap/dist/css/bootstrap.css";
import Link from "next/link";

export const NavMenu = ({ children }: { children: React.JSX.Element }) => {
  const onToggle = useCallback(() => {}, []);

  return (
    <>
      <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
          <div className="container">
            <Link className="navbar-brand" href="/">
              FrontEndSPA
            </Link>
            <button
              className="navbar-toggler"
              type="button"
              data-toggle="collapse"
              data-target=".navbar-collapse"
              aria-label="Toggle navigation"
              onClick={onToggle}
            >
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
              <ul className="navbar-nav flex-grow">
                <li className="nav-item">
                  <Link className="nav-link text-dark" href="/">
                    Home
                  </Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link text-dark" href="/sessions">
                    <span className="glyphicon glyphicon-th-list"></span>{" "}
                    Sessions
                  </Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link text-dark" href="/speakers">
                    <span className="glyphicon glyphicon-th-list"></span>{" "}
                    Speakers
                  </Link>
                </li>
              </ul>
            </div>
          </div>
        </nav>
      </header>
      {children}
    </>
  );
};
