"use client";
import Link from "next/link";
import { usePathname } from "next/navigation";

export default function NavBar() {
  const pathname = usePathname();
  return (
    <>
      <header
        className="relative flex w-full flex-wrap items-center justify-between bg-white-background
            flex-shrink-0 flex-none flexBetween"
      >
        <nav className="h-full w-full flex flex-1">
          <ul className="list-style-none flex ps-0 flex-row h-full w-full">
            <Link href="/" className="w-1/5 flex items-center pl-6">
              <img src="/images/logo.svg"></img>
            </Link>
            <Link
              href="/"
              className={`${
                pathname === "/" ? "bg-primary" : ""
              } px-4 h-full flex items-center hover:bg-primary mr-1`}
            >
              AVALEHT
            </Link>
            <Link
              href="/lisa-yritus"
              className={`${
                pathname === "/lisa-yritus" ? "bg-primary" : ""
              } px-4 h-full flex items-center hover:bg-primary`}
            >
              ÜRITUSE LISAMINE
            </Link>
            <div className="px-8 py-1 ml-auto">
              <img src="/images/symbol.svg"></img>
            </div>
          </ul>
        </nav>
      </header>
    </>
  );
}
