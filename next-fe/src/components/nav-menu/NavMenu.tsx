import { useCallback } from "react";
import "./NavMenu.scss";
import "bootstrap/dist/css/bootstrap.css";

export const NavMenu = () => {
  const onToggle = useCallback(() => {}, []);

  return (
    <header>
      <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
        <div className="container">
          <a className="navbar-brand">FrontEndSPA</a>
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
                <a className="nav-link text-dark">Home</a>
              </li>
              <li className="nav-item">
                <a className="nav-link text-dark">
                  <span className="glyphicon glyphicon-th-list"></span> Sessions
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link text-dark">
                  <span className="glyphicon glyphicon-th-list"></span> Speakers
                </a>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </header>
  );
};
