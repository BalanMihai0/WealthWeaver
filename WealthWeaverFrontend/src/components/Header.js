import React, { useState } from "react";
import {
  UserIcon,
  CreditCardIcon,
  CashIcon,
  PaperAirplaneIcon,
  MoonIcon,
  SunIcon,
} from "@heroicons/react/outline";
import { LibraryIcon } from "@heroicons/react/solid";
import { Link } from "react-router-dom";
import  Logo from '../assets/Logo.png'

/**
 * Header component that provides navigation and Dark Mode toggle button
 *  `active` determines the active nav item
 * @param {active}
 * @returns
 */
export default function Header({ active }) {
  const [dark, setDark] = useState(() => localStorage.theme === "dark");

  const isActive = (link) => {
    return active === link ? " bg-blue-500 text-white" : "";
  };

  /**Function that toggles the Dark/Light Mode */
  const toggleMode = () => {
    if (localStorage.theme === "dark") {
      document.documentElement.classList.remove("dark");
      localStorage.theme = "light";
      setDark(false);
    } else if (localStorage.theme === "light" || !("theme" in localStorage)) {
      document.documentElement.classList.add("dark");
      localStorage.theme = "dark";
      setDark(true);
    }
  };

  return (
    <header className="pb-8 font-mono text-white">
      <div className="bg-white h-16 shadow-lg rounded-lg flex items-center justify-between text-blue-500 dark:bg-gray-700 dark:text-white">
        <Link
          to="/"
          className="hidden lg:flex pl-2 text-xl text-blue-500  h-full justify-center items-center rounded-l-lg overflow-hidden"
        >
          <span className="px-2 bg-blue-500 rounded-full text-white w-24 h-24 -ml-4 flex items-center justify-center">
            <img src={Logo} alt="WealthWeaver Logo" className="w-16 h-16 object-contain" />
          </span>
          <span className="px-4">WealthWeaver</span>
        </Link>
        <Link
          to="/accounts"
          className={
            "flex h-full justify-center items-center px-4 cursor-pointer hover:bg-blue-500 hover:text-white gap-4 rounded-l-xl lg:rounded-none" +
            isActive("accounts")
          }
        >
          <CreditCardIcon className="h-6 w-6 " />
          <span className="hidden md:inline">ACCOUNTS</span>
        </Link>

        <Link
          to="/transactions"
          className={
            "flex h-full  justify-center items-center px-4 cursor-pointer hover:bg-blue-500 hover:text-white gap-4 " +
            isActive("transactions")
          }
        >
          <CashIcon className="h-6 w-6" />
          <span className="hidden md:inline">TRANSACTIONS</span>
        </Link>

        <Link
          to="/transfer"
          className={
            "flex h-full  justify-center items-center px-4 cursor-pointer hover:bg-blue-500 hover:text-white gap-4 " +
            isActive("transfer")
          }
        >
          <PaperAirplaneIcon className="h-6 w-6 rotate-45" />
          <span className="hidden md:inline">TRANSFERS</span>
        </Link>
        <span className="flex-auto hidden md:flex"></span>
        <Link
          to="/profile"
          className={
            "px-4 h-full flex justify-center items-center text-sm gap-4 cursor-pointer hover:bg-blue-500 hover:text-white " +
            isActive("profile")
          }
        >
          <UserIcon className="h-6 w-6" />
          <span className="hidden md:inline">PROFILE</span>
        </Link>
        <div
          onClick={toggleMode}
          className={
            "px-4 h-full flex justify-center items-center text-sm gap-4 cursor-pointer hover:bg-gray-700 hover:text-white rounded-r-xl "
          }
        >
          {dark ? (
            <SunIcon className="h-6 w-6" />
          ) : (
            <MoonIcon className="h-6 w-6" />
          )}
        </div>
      </div>
    </header>
  );
}
